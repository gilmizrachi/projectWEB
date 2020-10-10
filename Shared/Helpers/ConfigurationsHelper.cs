﻿using projectWEB.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectWEB.Helpers
{
    public static class ConfigurationsHelper
    {
        public static ConcurrentDictionary<string, string> ConfigurationsDictionary = new ConcurrentDictionary<string, string>();

        public static void UpdateConfigurations(List<Configuration> configurations)
        {
            if (configurations != null && configurations.Count > 0)
            {
                ConfigurationsDictionary = new ConcurrentDictionary<string, string>();

                foreach (var configuration in configurations)
                {
                    ConfigurationsDictionary.TryAdd(configuration.Key, configuration.Value);
                }
            }
        }

        public static void UpdateConfiguration(Configuration configuration)
        {
            if (configuration != null && ConfigurationsDictionary.ContainsKey(configuration.Key))
            {
                ConfigurationsDictionary[configuration.Key] = configuration.Value;
            }
        }

        public static T GetConfigValue<T>(string key)
        {
            if (!ConfigurationsDictionary.ContainsKey(key))
            {
                throw (new ApplicationException("No such Configuration found: " + key));
            }

            try
            {
                var value = ConfigurationsDictionary[key];

                if(!string.IsNullOrEmpty(value))
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception e)
            {
                throw (new ApplicationException(string.Format("Cannot convert Configuration value to {0}", typeof(T)), e));
            }
        }

        public static string ApplicationName
        {
            get
            {
                return (GetConfigValue<string>("ApplicationName"));

                //if (EnableMultilingual)
                //{
                //    return (GetConfigValue<string>("ApplicationName"));
                //}
                //else return (GetConfigValue<string>("ApplicationName"));
            }
        }

        public static string ApplicationIntro
        {
            get
            {
                return (GetConfigValue<string>("ApplicationIntro"));
            }
        }

        public static string AddressLine1
        {
            get
            {
                return (GetConfigValue<string>("AddressLine1"));
            }
        }

        public static string AddressLine2
        {
            get
            {
                return (GetConfigValue<string>("AddressLine2"));
            }
        }
        public static decimal FlatDeliveryCharges
        {
            get
            {
                return (GetConfigValue<decimal>("FlatDeliveryCharges"));
            }
        }
        public static string PhoneNumber
        {
            get
            {
                return (GetConfigValue<string>("PhoneNumber"));
            }
        }

        public static string MobileNumber
        {
            get
            {
                return (GetConfigValue<string>("MobileNumber"));
            }
        }

        public static string Email
        {
            get
            {
                return (GetConfigValue<string>("Email"));
            }
        }

        public static string AdminEmailAddress
        {
            get
            {
                return (GetConfigValue<string>("AdminEmailAddress"));
            }
        }
        public static string CurrencySymbol
        {
            get
            {
                return (GetConfigValue<string>("CurrencySymbol"));
            }
        }

        public static string PriceCurrencyPosition
        {
            get
            {
                return (GetConfigValue<string>("PriceCurrencyPosition"));
            }
        }

        public static bool EnableCreditCardPayment
        {
            get
            {
                return (GetConfigValue<bool>("EnableCreditCardPayment"));
            }
        }

        public static bool EnableCashOnDeliveryMethod
        {
            get
            {
                return (GetConfigValue<bool>("EnableCashOnDeliveryMethod"));
            }
        }

        public static bool EnablePayPalPaymentMethod
        {
            get
            {
                return (GetConfigValue<bool>("EnablePayPalPaymentMethod"));
            }
        }

        public static string PayPalClientID
        {
            get
            {
                return (GetConfigValue<string>("PayPalClientID"));
            }
        }
        
        public static int DigitsAfterDecimalPoint
        {
            get
            {
                return (GetConfigValue<int>("DigitsAfterDecimalPoint"));
            }
        }

        public static int DefaultRating
        {
            get
            {
                return 5;
            }
        }

        public static string AuthorizeNet_ApiLoginID
        {
            get
            {
                return (GetConfigValue<string>("AuthorizeNet_ApiLoginID"));
            }
        }

        public static string AuthorizeNet_ApiTransactionKey
        {
            get
            {
                return (GetConfigValue<string>("AuthorizeNet_ApiTransactionKey"));
            }
        }

        public static bool AuthorizeNet_SandBox
        {
            get
            {
                return (GetConfigValue<bool>("AuthorizeNet_SandBox"));
            }
        }

        public static string SendGrid_APIKey
        {
            get
            {
                return (GetConfigValue<string>("SendGrid_APIKey"));
            }
        }

        public static string SendGrid_FromEmailAddress
        {
            get
            {
                return (GetConfigValue<string>("SendGrid_FromEmailAddress"));
            }
        }

        public static string SendGrid_FromEmailAddressName
        {
            get
            {
                return (GetConfigValue<string>("SendGrid_FromEmailAddressName"));
            }
        }
        public static string WhatsAppNumber
        {
            get
            {
                return (GetConfigValue<string>("WhatsAppNumber"));
            }
        }
    }
}