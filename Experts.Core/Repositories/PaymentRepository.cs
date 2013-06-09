using System;
using Experts.Core.Data;
using Experts.Core.Entities;
namespace Experts.Core.Repositories
{
    public class PaymentRepository:AuditableRepository<Payment>
    {
        public PaymentRepository(DataContext db) : base(db) {}

        public IService GetPaymentRelatedService(string serviceType, int relatedId)
        {
            if (serviceType == typeof(Thread).FullName) return Db.Threads.Find(relatedId);

            throw new ArgumentOutOfRangeException(serviceType);
        }

        //public void UpdatePaymentRelatedService(IService relatedService)
        //{
        //    var t = relatedService as Thread;
        //    if (t != null) Db.Threads.Update(t);

        //    throw new ArgumentOutOfRangeException();
        //}

        //public decimal GetTotalValue(PaymentType type, int relatedId)
        //{
        //    switch (type)
        //    {
        //        case PaymentType.ForAnswer:
        //            return Db.Threads.Find(relatedId).Value;
        //        default:
        //            throw new ArgumentOutOfRangeException("type");
        //    }
        //}

        

        //public static bool IsPaymentObligatory<T>(T payableItem)
        //{
        //    var thread = payableItem as Thread;
        //    if(thread != null) 
        //        return thread.Value >= thread.Author.AvailableBalance;
                
        //    return false;
        //}

        //public void BlockAmountFromBalance(PaymentType type, int relatedId)
        //{
        //    var amountToBlock = GetTotalValue(type, relatedId) - GetPaymentValue(type, relatedId);

        //}





        //public object GetPaymentItem(PaymentType paymentType, int relatedId)
        //{
        //    if (paymentType == PaymentType.ForAnswer)
        //        return Db.Set<Thread>().Find(relatedId);

        //    return null;
        //}

        //public static decimal GetPaymentValue(object payableItem)
        //{
        //    var thread = payableItem as Thread;
        //    if(thread != null)
        //        return thread.Author.AvailableBalance > 0
        //            ? thread.Value - thread.Author.AvailableBalance
        //            : thread.Value;

        //    return decimal.MinusOne;
        //}

        //public static decimal GetMinimalPaymentValue<T>
    }
}
