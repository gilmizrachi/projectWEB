$(document).ready(function () {
    var request = new XMLHttpRequest();

    request.open('POST', 'https://private-anon-0b3459da1b-oxebox.apiary-proxy.com/pbrapi/alpha/v1/index.php/PBR/generateBill');

    request.setRequestHeader('Content-Type', 'application/json');
    request.setRequestHeader('Authorization', 'T0JQLUJBTUJBUy1JU1ItSVM6bHIyeGdLQ3N0dlpVUGZlM2tJSnpvaFFXYk84dTlkcEdFTVN5RllUcVhWUjFEbUhC');
    request.setRequestHeader('Content-Length', 'Payload length');

    request.onreadystatechange = function () {
        if (this.readyState === 4) {
            console.log('Status:', this.status);
            console.log('Headers:', this.getAllResponseHeaders());
            console.log('Body:', this.responseText);
        }
    };

    var body = {
        'CustomerDetails': {
            'Name': 'Customer Name',
            'Phone': '99xxxxxxxx',
            'CountryCode': 91,
            'Email': {
                'recepientEmail': 'alonbab@gmail.com',
                'subject': 'This is your email receipt subject line',
                'fromEmail': 'receipts@oxebox.com',
                'fromName': 'Jamies Grocery',
                'replyToEmail': 'customers@jamiesgrocery.com'
            }
        },
        'PartnerDetails': {
            'PartnerID': 'OBP-BAMBAS-ISR-IS',
            'AuthKey': 'lr2xgKCstvZUPfe3kIJzohQWbO8u9dpGEMSyFYTqXVR1DmHB'
        },
        'StoreDetails': {
            'StoreID': 'OBS-BAMBAS-ISR-IS'
        },
        'BillingDetails': {
            'BillReceiptID': 'Bill_1234',
            'TransactionDate': '2017-09-30',
            'TransactionTime': '22:10:01',
            'AdditionalDetails': [
                {
                    'Name': 'Table No',
                    'Value': '1234'
                },
                {
                    'Name': 'Order No',
                    'Value': '06161'
                }
            ],
            'PaymentDetails': [
                {
                    'Amount': 4000,
                    'Type': 'card',
                    'Cashier': 'John Rock'
                },
                {
                    'Amount': 1646,
                    'Type': 'cash',
                    'Cashier': 'John Rock'
                }
            ],
            'ItemDetails': [
                {
                    'ItemCode': 'Pizza - 01',
                    'ItemName': 'Exotica Supreme Pizza',
                    'ItemHeader': '12 medium, extra cheeze,olives',
                    'ItemQty': 20,
                    'ItemUnit': 'pcs',
                    'ItemPrice': 40,
                    'ItemTotal': 800,
                    'SubItems': [
                        {
                            'ItemName': 'Extra cheese',
                            'ItemQty': 1,
                            'ItemUnit': 'pcs',
                            'ItemPrice': 29,
                            'ItemTotal': 29
                        },
                        {
                            'ItemName': 'Extra Toppings',
                            'ItemQty': 2,
                            'ItemUnit': 'pcs',
                            'ItemPrice': 55,
                            'ItemTotal': 110
                        }
                    ]
                },
                {
                    'ItemCode': 'Pizza-02',
                    'ItemName': 'Triple chicken feast pizza',
                    'ItemHeader': 'Crust: Pan, Medium, spicy',
                    'ItemQty': 40,
                    'ItemUnit': 'pcs',
                    'ItemPrice': 100,
                    'ItemTotal': 4000,
                    'Discounts': [
                        {
                            'Name': 'Store Promo',
                            'Total': 99,
                            'Percent': 0
                        },
                        {
                            'Name': 'Additional discount',
                            'Total': 18,
                            'Percent': 9
                        }
                    ],
                    'Taxes': [
                        {
                            'Name': 'SGST 6',
                            'Total': 240,
                            'Percent': 6
                        },
                        {
                            'Name': 'SGST 9',
                            'Total': 240,
                            'Percent': 9
                        }
                    ]
                }
            ],
            'Discounts': [
                {
                    'Name': 'Store Promo',
                    'Total': 99,
                    'Percent': ''
                },
                {
                    'Name': 'Bulk discount',
                    'Total': 400,
                    'Percent': ''
                }
            ],
            'Taxes': [
                {
                    'Name': 'SGST',
                    'Total': 222.86,
                    'Percent': 6
                },
                {
                    'Name': 'CGST',
                    'Total': 222.86,
                    'Percent': 9
                }
            ],
            'SubTotal': 5600,
            'GrandTotal': 0,
            'RoundOff': 0.29,
            'TotalBillAmount': 5646,
            'AdditionalCharges': [
                {
                    'Name': 'Delivery Charges',
                    'Amount': 4000,
                    'Discounts': [
                        {
                            'Name': 'Store Promo',
                            'Total': 99,
                            'Percent': ''
                        },
                        {
                            'Name': 'Additional discount',
                            'Total': 18,
                            'Percent': 9
                        }
                    ],
                    'Taxes': [
                        {
                            'Name': 'CGST 9',
                            'Total': 7.63,
                            'Percent': 9
                        },
                        {
                            'Name': 'SGST 9',
                            'Total': 7.63,
                            'Percent': 9
                        }
                    ]
                }
            ],
            'BillingAddress': {
                'AddressLine1': 'address line 1',
                'AddressLine2': 'address line 2',
                'Area': 'BTM',
                'City': 'Bengalaru',
                'State': 'Karnataka',
                'Zip': 11111,
                'Country': 'India',
                'Name': 'Manoj Gupta',
                'Phone': '919xxxxxxxxx'
            },
            'ShippingAddress': {
                'AddressLine1': 'address line 1',
                'AddressLine2': 'address line 2',
                'Area': 'BTM',
                'City': 'Bengalaru',
                'State': 'Karnataka',
                'Zip': 11111,
                'Country': 'India',
                'Name': 'Manoj Gupta',
                'Phone': '919xxxxxxxxx'
            }
        }
    };

    request.send(JSON.stringify(body));
});

