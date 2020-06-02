using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovableBridges.Model;
using Newtonsoft.Json;
using SQLite;

namespace MovableBridges.DataLayer
{
    public class DBWrapper 
    {
        List<Bridge> bList = new List<Bridge>();        

        static readonly Lazy<SQLiteAsyncConnection> LazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.dBPath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database = LazyInitializer.Value;
        static bool initialized = false;

        public DBWrapper()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(NavigationOpening).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(NavigationOpening)).ConfigureAwait(false);
                    initialized = true;
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(District).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(District)).ConfigureAwait(false);

                    var ds = GetDistrictcItemsAsync().Result;
                    if (ds == null || ds.Count == 0)
                    {
                        var dList = DistrictList();
                        foreach (var district in dList)
                        {
                            await SaveDItemAsync(district, true);
                        }
                    }
                    initialized = true;
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Parish).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Parish)).ConfigureAwait(false);

                    var ds = GetParishItemsAsync().Result;
                    if (ds == null || ds.Count == 0)
                    {
                        var pList = ParishList();
                        foreach (var parish in pList)
                        {
                            await SavePItemAsync(parish, true);
                        }
                    }
                    initialized = true;
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Bridge).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Bridge)).ConfigureAwait(false);                    

                    var ds = GetBridgeItemsAsync().Result;
                    if (ds == null || ds.Count == 0)
                    {
                        bList = BridgeList();
                        foreach (var bridge in bList)
                        {
                            await SaveBItemAsync(bridge, true);
                        }
                    }
                    initialized = true;
                }               
               
            }
        }
        
        public Task<List<NavigationOpening>> GetItemsAsync()
        {
            return Database.Table<NavigationOpening>().ToListAsync();
        }

        public Task<List<Bridge>> GetBridgeItemsAsync()
        {
            return Database.Table<Bridge>().ToListAsync();
        }

        public Task<List<Parish>> GetParishItemsAsync()
        {
            return Database.Table<Parish>().ToListAsync();
        }

        public Task<List<District>> GetDistrictcItemsAsync()
        {
            return Database.Table<District>().ToListAsync();
        }
        public Task<List<NavigationOpening>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<NavigationOpening>("SELECT * FROM [Bridges] WHERE [Done] = 0");
        }

        public Task<NavigationOpening> GetItemAsync(int id)
        {
            return Database.Table<NavigationOpening>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(NavigationOpening opening)
        {
            if (opening.ID != 0)
            {
                return Database.UpdateAsync(opening);
            }
            else
            {
                return Database.InsertAsync(opening);
            }
        }

        public Task<int> SaveBItemAsync(Bridge bridge, bool init = false)
        {
            if (bridge.ID != 0 && !init)
            {
                return Database.UpdateAsync(bridge);
            }
            else
            {
                return Database.InsertAsync(bridge);
            }
        }

        public Task<int> SavePItemAsync(Parish parish, bool init = false)
        {
            if (parish.ID != 0 && !init)
            {
                return Database.UpdateAsync(parish);
            }
            else
            {
                return Database.InsertAsync(parish);
            }
        }

        public Task<int> SaveDItemAsync(District district, bool init = false)
        {
            if (district.ID != 0 && !init)
            {
                return Database.UpdateAsync(district);
            }
            else
            {
                return Database.InsertAsync(district);
            }
        }

        public Task<int> DeleteItemAsync(NavigationOpening opening)
        {
            return Database.DeleteAsync(opening);
        }

        public Task<int> DeleteBItemAsync(Bridge bridge)
        {
            return Database.DeleteAsync(bridge);
        }

        public List<District> DistrictList()
        {
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("http://172.17.133.81:8080/api");
            HttpResponseMessage response = httpclient.GetAsync(httpclient.BaseAddress + "/values/GetDistricts").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<District> dList = JsonConvert.DeserializeObject<List<District>>(data);
            return dList;
            //List<District> dsList = new List<District>();
            //dsList.Add(new District { ID = 1, District_Name = "District 02" });
            //dsList.Add(new District { ID = 2, District_Name = "District 03" });
            //dsList.Add(new District { ID = 3, District_Name = "District 05" });
            //dsList.Add(new District { ID = 4, District_Name = "District 07" });
            //dsList.Add(new District { ID = 5, District_Name = "District 08" });
            //dsList.Add(new District { ID = 6, District_Name = "District 58" });
            //dsList.Add(new District { ID = 7, District_Name = "District 61" });
            //dsList.Add(new District { ID = 8, District_Name = "District 62" });

            //return dsList;
        }

        public List<Parish> ParishList()
        {
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("http://172.17.133.81:8080/api");
            HttpResponseMessage response = httpclient.GetAsync(httpclient.BaseAddress + "/values/GetParishes").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<Parish> pList = JsonConvert.DeserializeObject<List<Parish>>(data);
            return pList;
            //List<Parish> cList = new List<Parish>();
            //cList.Add(new Parish { ID = 1, Parish_Name = "JEFFERSON", District_ID = 1 });
            //    cList.Add(new Parish { ID = 2, Parish_Name = "LAFOURCHE", District_ID = 1 });
            //    cList.Add(new Parish { ID = 3, Parish_Name = "TERREBONNE", District_ID = 1 });
            //    cList.Add(new Parish { ID = 4, Parish_Name = "ORLEANS", District_ID = 1 });
            //    cList.Add(new Parish { ID = 5, Parish_Name = "ST. CHARLES", District_ID = 1 });
            //    cList.Add(new Parish { ID = 6, Parish_Name = "ACADIA", District_ID = 2 });
            //    cList.Add(new Parish { ID = 7, Parish_Name = "IBERIA", District_ID = 2 });
            //    cList.Add(new Parish { ID = 8, Parish_Name = "LAFAYETTE", District_ID = 2 });
            //    cList.Add(new Parish { ID = 9, Parish_Name = "ST. MARTIN", District_ID = 2 });
            //    cList.Add(new Parish { ID = 10, Parish_Name = "ST. MARY", District_ID = 2 });
            //    cList.Add(new Parish { ID = 11, Parish_Name = "VERMILION", District_ID = 2 });
            //    cList.Add(new Parish { ID = 12, Parish_Name = "OUACHITA", District_ID = 3 });
            //    cList.Add(new Parish { ID = 13, Parish_Name = "CALCASIEU", District_ID = 4 });
            //    cList.Add(new Parish { ID = 14, Parish_Name = "CAMERON", District_ID = 4 });
            //    cList.Add(new Parish { ID = 15, Parish_Name = "RAPIDES", District_ID = 5 });
            //    cList.Add(new Parish { ID = 16, Parish_Name = "CATAHOULA", District_ID = 6 });
            //    cList.Add(new Parish { ID = 17, Parish_Name = "ASSUMPTION", District_ID = 7 });
            //    cList.Add(new Parish { ID = 18, Parish_Name = "IBERVILLE", District_ID = 7 });
            //    cList.Add(new Parish { ID = 19, Parish_Name = "POINTE COUPEE", District_ID = 7 });
            //    cList.Add(new Parish { ID = 20, Parish_Name = "LIVINGSTON", District_ID = 8 });
            //    cList.Add(new Parish { ID = 21, Parish_Name = "ST. TAMMANY", District_ID = 8 });
            //return cList;
        }
        public List<Bridge> BridgeList()
        {
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("http://172.17.133.81:8080/api");
            HttpResponseMessage response = httpclient.GetAsync(httpclient.BaseAddress + "/values").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            List<Bridge> oList = JsonConvert.DeserializeObject<List<Bridge>>(data);
            return oList;
            //bList.Add(new Bridge {Recall_Number = "000810", Bridge_Name = "BARATARIA - BAYOU BARATARIA", Type_Draw = "SWING - ELEC.", Tenders = 4, MilePoint = 35.7, StateRoute = "302", Parish_Id = 1, User_Modified="Edula", Date_Created =DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "005322", Bridge_Name = "ESTHERWOOD PONTOON BAYOU PLAQ.BRULE", Type_Draw = "PONTOON - ELEC.", Tenders = 0, MilePoint = 8.0, StateRoute = "91", Parish_Id = 6, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "200940", Bridge_Name = "BAYOU BLUE PONTOON-INTERCOASTAL CANAL", Type_Draw = "PONTOON - ELEC.", Tenders = 4, MilePoint = 49.0, StateRoute = "316", Parish_Id = 2, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "003450", Bridge_Name = "BOUDREAUX -BOUDREAUXCANAL", Type_Draw = "SWING-HYD.", Tenders = 4, MilePoint = 0.3, StateRoute = "56", Parish_Id = 3, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "015390", Bridge_Name = "CHEF -CHEF MENTEUR", Type_Draw = "SWING-ELEC.", Tenders = 4, MilePoint = 2.8, StateRoute = "US 90", Parish_Id = 4, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "002830", Bridge_Name = "DES ALLEMANDS - BAYOUDES ALLEMANDS", Type_Draw = "V.LIFT - ELEC.", Tenders = 3, MilePoint = 13.9, StateRoute = "631", Parish_Id = 5, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "030312", Bridge_Name = "DELCAMBRE BAYOUCARLIN", Type_Draw = "V.LIFT - ELEC.", Tenders = 4, MilePoint = 6.4, StateRoute = "14", Parish_Id = 7, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "006520", Bridge_Name = "MILTON VERMILIONRIVER", Type_Draw = "V.LIFT - ELEC.", Tenders = 1, MilePoint = 37.6, StateRoute = "92", Parish_Id = 8, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "008640", Bridge_Name = "KEYSTONE 92 EXT.BRIDGE", Type_Draw = "V.LIFT - ELEC.", Tenders = 0, MilePoint = 73.2, StateRoute = "92", Parish_Id = 9, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //bList.Add(new Bridge {Recall_Number = "030351", Bridge_Name = "LOUISA INTRACOSTALCANAL", Type_Draw = "DBL.BASC.-ELEC.", Tenders = 4, MilePoint = 134.0, StateRoute = "319", Parish_Id = 10, User_Modified = "Edula", Date_Created = DateTime.Now, Date_Modified = DateTime.Now });
            //return bList;
        }

    }
}
