using System;
using Experts.Core.Entities;

namespace Experts.Core.Utils
{
    public static class ThreadValueHelper
    {
        public static Provision GetExpertBrokerStandardProvision()
        {
            return RepositoryHelper.Repository.Provision.Find(p => p.IntProvisionPartner == (int)ProvisionPartner.StandardPartner);
        }

        public static decimal GetExpertProvision(Provision provision)
        {
            return provision.ProvisionValue;
        }

        public static decimal CalculatePotentialExpertValue(this Thread thread, Expert expert)
        {
            var baseValue = GetExpertProvision(expert.Provision) * thread.Value;
            return Decimal.Round(baseValue, 2, MidpointRounding.AwayFromZero) + thread.AdditionalExpertProvisionValue;
        }

        public static decimal CalculateExpertValue(this AdditionalService additionalService)
        {
            var baseValue = GetExpertProvision(additionalService.Expert.Provision) * additionalService.Value;
            return Decimal.Round(baseValue, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal CalculateValueBasedOnExpertLevel(decimal value, Expert expert)
        {
            return Decimal.Round(value * GetExpertProvision(expert.Provision), 2, MidpointRounding.AwayFromZero);
        }

        public static decimal CalculateUserValue(decimal expertValue, Expert expert)
        {
            return Decimal.Round(expertValue/GetExpertProvision(expert.Provision), 2);
        }

        public static decimal ExpertProvisionValue(this Thread thread)
        {
            return Decimal.Round(thread.Value * thread.ExpertProvision, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal FullExpertProvisionValue(this Thread thread)
        {
            return thread.ExpertProvisionValue() + thread.AdditionalExpertProvisionValue;
        }

        public static decimal BrokerProvisionValue(this Thread thread)
        {
            return Decimal.Round((thread.Value - thread.ExpertProvisionValue()) * thread.BrokerProvision, 2, MidpointRounding.AwayFromZero);
        }

        public static void CalculateAdditionalExpertProvision(this Thread thread, decimal newExpertValue)
        {
            var increase = newExpertValue - thread.CalculatePotentialExpertValue(thread.Expert);
            thread.AdditionalExpertProvisionValue += increase;
        }
    }
}
