using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;
using PharmacyManagermentSystem.Services.MiniServiceUpload;

namespace PharmacyManagermentSystem.Services.MiniServicePrescription
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly MyDbContext _Dbcontext;
        private readonly IUploadService _uploadService;
        public PrescriptionService(MyDbContext Dbcontext, IUploadService uploadService)
        {
            _Dbcontext = Dbcontext;
            _uploadService = uploadService;
        }
       
        public async Task<PrescriptionResponse> CreatePrescription(CreatePrescriptionRequest request)
        {
            using( var transaction = _Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    var result = await _uploadService.UploadImage(request.File);
                    if(result == null)
                    {
                        return null;
                    }
                    var prescription = new Prescription()
                    {
                        Id = request.Id,
                        ImageId = result.PublicId,
                        Image = result.Url,
                        CustomerId = request.CustomerId,
                        DoctorId = request.DoctorId
                    };

                    await _Dbcontext.Prescriptions.AddAsync(prescription);
                    await _Dbcontext.SaveChangesAsync();
                    transaction.Commit();
                    return new PrescriptionResponse()
                    {
                        Id = prescription.Id,
                        ImageId = result.PublicId,
                        Image = prescription.Image,
                        CustomerId = prescription.CustomerId,
                        DoctorId = prescription.DoctorId
                    };
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public async Task<bool> DeletePrescription(string id)
        {
            using( var transaction = _Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    var prescription = await _Dbcontext.Prescriptions.FindAsync(id);
                    if(prescription == null)
                    {
                        return false;
                    }
                    var deleteResult = await _uploadService.DeleteImage(prescription.ImageId);
                    _Dbcontext.Prescriptions.Remove(prescription);
                    await _Dbcontext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<PrescriptionResponse> UpdatePrescription(UpdatePrescriptionRequest request,string id)
        {
            using( var transaction =await _Dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var prescription = await _Dbcontext.Prescriptions.FindAsync(id);
                    if(prescription == null)
                    {
                        return null;
                    }
                    var result = await _uploadService.UploadImage(request.File);
                    if(result == null)
                    {
                        return null;
                    }
                    var deleteResult = await _uploadService.DeleteImage(prescription.ImageId);
                    if (deleteResult == null || deleteResult.Result != "ok")
                    {
                        return null;
                    }

                    prescription.ImageId = result.PublicId;
                    prescription.Image = result.Url;
                    await _Dbcontext.SaveChangesAsync();
                    transaction.Commit();
                    return new PrescriptionResponse()
                    {
                        Id= prescription.Id,
                        ImageId = result.PublicId,
                        Image = prescription.Image,
                        CustomerId = prescription.CustomerId,
                        DoctorId = prescription.DoctorId
                    };
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return null;
                }
            }
        }
        public async Task<List<Prescription>> GetAll()
        {
           return await _Dbcontext.Prescriptions.ToListAsync();
        }
    }
}
