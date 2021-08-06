using System;
using System.Linq;
using FinanceManagement.Class;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FinanceManagement.Function
{
    public static class DatabaseHandler
    {

        #region MMCustomerMaster

        public static List<MMCustomerMaster> GetCustomerUsed()
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            IQueryable<MMCustomerMaster> customerUseds = from temp in hQDataDataContext.MMCustomerMasters
                                                         where temp.IsUse
                                                         select temp;

            if (customerUseds == null)
                return null;
            return customerUseds.ToList();
        }

        public static List<MMCustomerMaster>[] GetCustomerMaster()
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var customerUseds = from temp in hQDataDataContext.MMCustomerMasters
                                where temp.IsUse == true
                                select temp;

            var customerUseless = from temp in hQDataDataContext.MMCustomerMasters
                                where temp.IsUse == false
                                select temp;

            List<MMCustomerMaster>[] customerMasters = {customerUseds.ToList(), customerUseless.ToList() };
            return customerMasters;
        }

        #endregion


        #region FixedFee

        public static List<string> GetDepartmentMaster()
        {
            return new List<string>()
            {
                "全部",
                "工事",
                "太田",
                "SDC",
                "本社",
            };
        }

        public static ObservableCollection<MMFixedFee> GetFixedFee(string feeName, string feeType, string department)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var result = hQDataDataContext.MMFixedFees.Where(x => x.FeeName == feeName && x.FeeType == feeType && x.Department == department).ToList();

            if(result.Count == 0)
            {
                return null;
            }

            return new ObservableCollection<MMFixedFee>(result);
        }

        public static List<MMFixedFee> GetFixedFee()
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();
            return hQDataDataContext.MMFixedFees.ToList();
        }


        public static ObservableCollection<MMFee> GetFixedFee(string feeName, string feeType, int yearNow)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            List<MMFee> mMFees = new List<MMFee>();

            if(string.IsNullOrEmpty(feeType))
            {
                feeType = feeName;
            }

            var result = hQDataDataContext.MMFixedFees.Where(x => x.FeeName == feeName && x.FeeType == feeType).ToList();

            foreach(var temp in result)
            {
                int yearFrom = temp.TimeFrom.Year;
                int monthFrom = temp.TimeFrom.Month;

                int yearTo = temp.TimeTo.Year;
                int monthTo = temp.TimeTo.Month;

                MMFee mMFee = new MMFee()
                {
                    FeeName = temp.FeeName,
                    Department = temp.Department,
                    FeeType = temp.FeeType,
                    Item = temp.Item,
                    Field1 = temp.Field1,
                    Field2 = temp.Field2,
                    Field3 = temp.Field3,
                    Field4 = temp.Field4,
                    Field5 = temp.Field5,
                    Field6 = temp.Field6,
                    Field7 = temp.Field7,
                    Field8 = temp.Field8,
                    Field9 = temp.Field9,
                    Year = yearNow,
                    Month1 = ((yearTo * 12 + monthTo - (yearNow + 1) * 12 - 1 >= 0) && ((yearNow + 1) * 12 + 1 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month2 = ((yearTo * 12 + monthTo - (yearNow + 1) * 12 - 2 >= 0) && ((yearNow + 1) * 12 + 2 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month3 = ((yearTo * 12 + monthTo - (yearNow + 1) * 12 - 3 >= 0) && ((yearNow + 1) * 12 + 3 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month4 = ((yearTo * 12 + monthTo - yearNow * 12 - 4 >= 0) && (yearNow * 12 + 4 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month5 = ((yearTo * 12 + monthTo - yearNow * 12 - 5 >= 0) && (yearNow * 12 + 5 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month6 = ((yearTo * 12 + monthTo - yearNow * 12 - 6 >= 0) && (yearNow * 12 + 6 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month7 = ((yearTo * 12 + monthTo - yearNow * 12 - 7 >= 0) && (yearNow * 12 + 7 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month8 = ((yearTo * 12 + monthTo - yearNow * 12 - 8 >= 0) && (yearNow * 12 + 8 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month9 = ((yearTo * 12 + monthTo - yearNow * 12 - 9 >= 0) && (yearNow * 12 + 9 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month10 = ((yearTo * 12 + monthTo - yearNow * 12 - 10 >= 0) && (yearNow * 12 + 10 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month11 = ((yearTo * 12 + monthTo - yearNow * 12 - 11 >= 0) && (yearNow * 12 + 11 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                    Month12 = ((yearTo * 12 + monthTo - yearNow * 12 - 12 >= 0) && (yearNow * 12 + 12 - yearFrom * 12 - monthFrom >= 0)) ? temp.Amount : 0,
                };

                mMFee.Sum = mMFee.Month1 + mMFee.Month2 + mMFee.Month3 + mMFee.Month4 + mMFee.Month5 + mMFee.Month6 + mMFee.Month7 + mMFee.Month8 + mMFee.Month9 + mMFee.Month10 + mMFee.Month11 + mMFee.Month12;

                mMFees.Add(mMFee);
            }

            return result.Count == 0 ? null : AddSumToMMFee(feeName, feeType, yearNow, mMFees);
        }


        public static bool DeleteFixedFee(string feeName, string feeType, string department)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                var deleteValues = hQDataDataContext.MMFixedFees.Where(x => x.FeeName == feeName && x.Department == department && x.FeeType == feeType);

                foreach (var temp in deleteValues)
                {
                    hQDataDataContext.MMFixedFees.DeleteOnSubmit(temp);
                }

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool OverwriteFixedFee(ObservableCollection<MMFixedFee> mMFixedFees)
        {
            try
            {
                if (mMFixedFees == null) return false;

                MMFixedFee mMFixedFee = mMFixedFees.FirstOrDefault();

                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                var deleteValues = hQDataDataContext.MMFixedFees.Where(x => x.FeeName == mMFixedFee.FeeName && x.Department == mMFixedFee.Department && x.FeeType == mMFixedFee.FeeType); // && x.Item == mMFixedFee.Item);

                //if (mMFixedFee.Field1 != null) { deleteValues = deleteValues.Where(x => x.Field1 == mMFixedFee.Field1); }
                //if (mMFixedFee.Field2 != null) { deleteValues = deleteValues.Where(x => x.Field2 == mMFixedFee.Field2); }
                //if (mMFixedFee.Field3 != null) { deleteValues = deleteValues.Where(x => x.Field3 == mMFixedFee.Field3); }
                //if (mMFixedFee.Field4 != null) { deleteValues = deleteValues.Where(x => x.Field4 == mMFixedFee.Field4); }
                //if (mMFixedFee.Field5 != null) { deleteValues = deleteValues.Where(x => x.Field5 == mMFixedFee.Field5); }
                //if (mMFixedFee.Field6 != null) { deleteValues = deleteValues.Where(x => x.Field6 == mMFixedFee.Field6); }
                //if (mMFixedFee.Field7 != null) { deleteValues = deleteValues.Where(x => x.Field7 == mMFixedFee.Field7); }
                //if (mMFixedFee.Field8 != null) { deleteValues = deleteValues.Where(x => x.Field8 == mMFixedFee.Field8); }
                //if (mMFixedFee.Field9 != null) { deleteValues = deleteValues.Where(x => x.Field9 == mMFixedFee.Field9); }

                foreach (var temp in deleteValues)
                {
                    hQDataDataContext.MMFixedFees.DeleteOnSubmit(temp);
                }

                foreach (var temp in mMFixedFees)
                {
                    hQDataDataContext.MMFixedFees.InsertOnSubmit(temp);
                }

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion


        #region MMFee

        public static ObservableCollection<MMFee> GetFee(string feeName, string feeType, int year)
        {

            if (string.IsNullOrEmpty(feeName)) return null;

            if (string.IsNullOrEmpty(feeType)) feeType = feeName;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var query = from temp in hQDataDataContext.MMFees
                        where temp.Year == year && temp.FeeName == feeName //gt && temp.FeeType == feeType
                        select temp;

            if (query == null) return null;

            if(feeType != "合計" && !string.IsNullOrEmpty(feeType))
            {
                query = query.Where(x => x.FeeType == feeType);
            }

            return AddSumToMMFee(feeName, feeType, year, query.ToList());

            //ObservableCollection<MMFee> workFees = new ObservableCollection<MMFee>();

            //var departments = query.Select(x => x.Department).Distinct();

            //if (departments == null) return null;

            //MMFee mMFeeSum = new MMFee()
            //{
            //    FeeName = feeName,
            //    Department = "合計",
            //    FeeType = feeType,
            //    Item = "合計",
            //    Year = year
            //};

            //foreach (string department in departments)
            //{

            //    var queryDepartment = query.Where(x => x.Department == department);

            //    var queryItems = queryDepartment.Select(x => x.Item).Distinct();

            //    MMFee mMFeeDepartment = new MMFee()
            //    {
            //        FeeName = feeName,
            //        Department = "合計",
            //        FeeType = feeType,
            //        Item = department + "合計",
            //        Year = year
            //    };

            //    foreach (string queryItem in queryItems)
            //    {
            //        var items = queryDepartment.Where(x => x.Item == queryItem);

            //        if (items.Count() == 0) break;

            //        MMFee mMFee = new MMFee()
            //        {
            //            FeeName = feeName,
            //            Department = department,
            //            FeeType = feeType,
            //            Item = items.FirstOrDefault().Item,
            //            Field1 = items.FirstOrDefault().Field1,
            //            Field2 = items.FirstOrDefault().Field2,
            //            Field3 = items.FirstOrDefault().Field3,
            //            Field4 = items.FirstOrDefault().Field4,
            //            Field5 = items.FirstOrDefault().Field5,
            //            Field6 = items.FirstOrDefault().Field6,
            //            Field7 = items.FirstOrDefault().Field7,
            //            Field8 = items.FirstOrDefault().Field8,
            //            Field9 = items.FirstOrDefault().Field9,
            //            Year = year
            //        };

            //        foreach (var item in items)
            //        {
            //            item.Sum = item.Month1 + item.Month2 + item.Month3 + item.Month4 + item.Month5 + item.Month6 + item.Month7 + item.Month8 + item.Month9 + item.Month10 + item.Month11 + item.Month12;

            //            mMFee.Month1    += item.Month1;
            //            mMFee.Month2    += item.Month2;
            //            mMFee.Month3    += item.Month3;
            //            mMFee.Month4    += item.Month4;
            //            mMFee.Month5    += item.Month5;
            //            mMFee.Month6    += item.Month6;
            //            mMFee.Month7    += item.Month7;
            //            mMFee.Month8    += item.Month8;
            //            mMFee.Month9    += item.Month9;
            //            mMFee.Month10   += item.Month10;
            //            mMFee.Month11   += item.Month11;
            //            mMFee.Month12   += item.Month12;
            //            mMFee.Sum       += item.Sum;
            //        }

            //        workFees.Add(mMFee);

            //        mMFeeDepartment.Month1 += mMFee.Month1;
            //        mMFeeDepartment.Month2 += mMFee.Month2;
            //        mMFeeDepartment.Month3 += mMFee.Month3;
            //        mMFeeDepartment.Month4 += mMFee.Month4;
            //        mMFeeDepartment.Month5 += mMFee.Month5;
            //        mMFeeDepartment.Month6 += mMFee.Month6;
            //        mMFeeDepartment.Month7 += mMFee.Month7;
            //        mMFeeDepartment.Month8 += mMFee.Month8;
            //        mMFeeDepartment.Month9 += mMFee.Month9;
            //        mMFeeDepartment.Month10 += mMFee.Month10;
            //        mMFeeDepartment.Month11 += mMFee.Month11;
            //        mMFeeDepartment.Month12 += mMFee.Month12;
            //        mMFeeDepartment.Sum += mMFee.Sum;

            //    }

            //    workFees.Add(mMFeeDepartment);

            //    mMFeeSum.Month1 += mMFeeDepartment.Month1;
            //    mMFeeSum.Month2 += mMFeeDepartment.Month2;
            //    mMFeeSum.Month3 += mMFeeDepartment.Month3;
            //    mMFeeSum.Month4 += mMFeeDepartment.Month4;
            //    mMFeeSum.Month5 += mMFeeDepartment.Month5;
            //    mMFeeSum.Month6 += mMFeeDepartment.Month6;
            //    mMFeeSum.Month7 += mMFeeDepartment.Month7;
            //    mMFeeSum.Month8 += mMFeeDepartment.Month8;
            //    mMFeeSum.Month9 += mMFeeDepartment.Month9;
            //    mMFeeSum.Month10 += mMFeeDepartment.Month10;
            //    mMFeeSum.Month11 += mMFeeDepartment.Month11;
            //    mMFeeSum.Month12 += mMFeeDepartment.Month12;
            //    mMFeeSum.Sum += mMFeeDepartment.Sum;

            //}

            //if (departments.Count() != 0)
            //    workFees.Add(mMFeeSum);
            //return workFees;
        }


        public static ObservableCollection<MMFee> AddSumToMMFee(string feeName, string feeType, int year, List<MMFee> mMFees)
        {
            ObservableCollection<MMFee> workFees = new ObservableCollection<MMFee>();

            var departments = mMFees.Select(x => x.Department).Distinct();

            MMFee mMFeeSum = new MMFee()
            {
                FeeName = feeName,
                Department = "合計",
                FeeType = feeType,
                Item = "合計",
                Year = year
            };

            foreach (string department in departments)
            {

                var queryDepartment = mMFees.Where(x => x.Department == department);

                var queryItems = queryDepartment.Select(x => x.Item).Distinct();

                MMFee mMFeeDepartment = new MMFee()
                {
                    FeeName = feeName,
                    Department = "合計",
                    FeeType = feeType,
                    Item = department + "合計",
                    Year = year
                };

                foreach (string queryItem in queryItems)
                {
                    var items = queryDepartment.Where(x => x.Item == queryItem);

                    if (items.Count() == 0) break;

                    MMFee mMFee = new MMFee()
                    {
                        FeeName = feeName,
                        Department = department,
                        FeeType = feeType,
                        Item = items.FirstOrDefault().Item,
                        Field1 = items.FirstOrDefault().Field1,
                        Field2 = items.FirstOrDefault().Field2,
                        Field3 = items.FirstOrDefault().Field3,
                        Field4 = items.FirstOrDefault().Field4,
                        Field5 = items.FirstOrDefault().Field5,
                        Field6 = items.FirstOrDefault().Field6,
                        Field7 = items.FirstOrDefault().Field7,
                        Field8 = items.FirstOrDefault().Field8,
                        Field9 = items.FirstOrDefault().Field9,
                        Year = year
                    };

                    foreach (var item in items)
                    {
                        item.Sum = item.Month1 + item.Month2 + item.Month3 + item.Month4 + item.Month5 + item.Month6 + item.Month7 + item.Month8 + item.Month9 + item.Month10 + item.Month11 + item.Month12;

                        mMFee.Month1    += item.Month1;
                        mMFee.Month2    += item.Month2;
                        mMFee.Month3    += item.Month3;
                        mMFee.Month4    += item.Month4;
                        mMFee.Month5    += item.Month5;
                        mMFee.Month6    += item.Month6;
                        mMFee.Month7    += item.Month7;
                        mMFee.Month8    += item.Month8;
                        mMFee.Month9    += item.Month9;
                        mMFee.Month10   += item.Month10;
                        mMFee.Month11   += item.Month11;
                        mMFee.Month12   += item.Month12;
                        mMFee.Sum       += item.Sum;
                    }

                    workFees.Add(mMFee);

                    mMFeeDepartment.Month1 += mMFee.Month1;
                    mMFeeDepartment.Month2 += mMFee.Month2;
                    mMFeeDepartment.Month3 += mMFee.Month3;
                    mMFeeDepartment.Month4 += mMFee.Month4;
                    mMFeeDepartment.Month5 += mMFee.Month5;
                    mMFeeDepartment.Month6 += mMFee.Month6;
                    mMFeeDepartment.Month7 += mMFee.Month7;
                    mMFeeDepartment.Month8 += mMFee.Month8;
                    mMFeeDepartment.Month9 += mMFee.Month9;
                    mMFeeDepartment.Month10 += mMFee.Month10;
                    mMFeeDepartment.Month11 += mMFee.Month11;
                    mMFeeDepartment.Month12 += mMFee.Month12;
                    mMFeeDepartment.Sum += mMFee.Sum;

                }

                workFees.Add(mMFeeDepartment);

                mMFeeSum.Month1 += mMFeeDepartment.Month1;
                mMFeeSum.Month2 += mMFeeDepartment.Month2;
                mMFeeSum.Month3 += mMFeeDepartment.Month3;
                mMFeeSum.Month4 += mMFeeDepartment.Month4;
                mMFeeSum.Month5 += mMFeeDepartment.Month5;
                mMFeeSum.Month6 += mMFeeDepartment.Month6;
                mMFeeSum.Month7 += mMFeeDepartment.Month7;
                mMFeeSum.Month8 += mMFeeDepartment.Month8;
                mMFeeSum.Month9 += mMFeeDepartment.Month9;
                mMFeeSum.Month10 += mMFeeDepartment.Month10;
                mMFeeSum.Month11 += mMFeeDepartment.Month11;
                mMFeeSum.Month12 += mMFeeDepartment.Month12;
                mMFeeSum.Sum += mMFeeDepartment.Sum;

            }

            if (departments.Count() != 0)
            {
                workFees.Add(mMFeeSum);
            }

            return workFees;
        }


        public static ObservableCollection<MMFeeTypeStruct> GetFeeTypeStruct(string FeeName)
        {
            if (string.IsNullOrEmpty(FeeName)) return null;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var query = from feeStruct in hQDataDataContext.MMFeeStructs
                        join feeTypeStruct in hQDataDataContext.MMFeeTypeStructs
                        on feeStruct.Id equals feeTypeStruct.FeeId
                        where feeStruct.FeeName == FeeName
                        select feeTypeStruct;

            if (query == null) return null;
            return new ObservableCollection<MMFeeTypeStruct>(query);
        }

        public static ObservableCollection<MMFee> AutoGetFee(string feeName, string feeType, int year )
        {

            if (string.IsNullOrEmpty(feeName)) return null;

            if (string.IsNullOrEmpty(feeType))
                feeType = feeName;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var query = from temp in hQDataDataContext.MMFees
                        where temp.FeeName == feeName && temp.FeeType == feeType
                        select temp;

            var tempQuery = query.GroupBy(x => x.Year)
                .OrderByDescending(gr => gr.Count())
                .Select(y => y.Key);

            if (tempQuery.Count() == 0) return null;

            int tempYear = tempQuery.FirstOrDefault();

            ObservableCollection<MMFee> mMFees = GetFee(feeName, feeType, tempYear);

            foreach (var temp in mMFees)
                temp.Year = year;

            return mMFees;

        }

        public static void UpdateSumInFee(ref ObservableCollection<MMFee> mMFees)
        {
            if (mMFees == null) return;

            string feeType = mMFees.FirstOrDefault().FeeType;
            int year = mMFees.FirstOrDefault().Year;

            var departments = mMFees.Select(x => x.Department).Distinct();

            if (departments == null) return;

            int sumMonth1 = 0;
            int sumMonth2 = 0;
            int sumMonth3 = 0;
            int sumMonth4 = 0;
            int sumMonth5 = 0;
            int sumMonth6 = 0;
            int sumMonth7 = 0;
            int sumMonth8 = 0;
            int sumMonth9 = 0;
            int sumMonth10 = 0;
            int sumMonth11 = 0;
            int sumMonth12 = 0;
            int sumSum = 0;

            foreach (string department in departments)
            {
                if (department == "合計") continue;

                var queryItems = mMFees.Where(x => x.Department == department);

                int month1 = 0;
                int month2 = 0;
                int month3 = 0;
                int month4 = 0;
                int month5 = 0;
                int month6 = 0;
                int month7 = 0;
                int month8 = 0;
                int month9 = 0;
                int month10 = 0;
                int month11 = 0;
                int month12 = 0;
                int sum = 0;

                foreach(var item in queryItems)
                {
                    month1 += item.Month1;
                    month2 += item.Month2;
                    month3 += item.Month3;
                    month4 += item.Month4;
                    month5 += item.Month5;
                    month6 += item.Month6;
                    month7 += item.Month7;
                    month8 += item.Month8;
                    month9 += item.Month9;
                    month10 += item.Month10;
                    month11 += item.Month11;
                    month12 += item.Month12;

                    item.Sum = item.Month1 + item.Month2 + item.Month3 + item.Month4 + item.Month5 + item.Month6 + item.Month7 + item.Month8 + item.Month9 + item.Month10 + item.Month11 + item.Month12;

                    sum += item.Sum;
                }

                foreach(var temp in mMFees)
                {
                    if(temp.Department == "合計" && temp.Item.Contains(department))
                    {
                        temp.Month1 = month1;
                        temp.Month2 = month2;
                        temp.Month3 = month3;
                        temp.Month4 = month4;
                        temp.Month5 = month5;
                        temp.Month6 = month6;
                        temp.Month7 = month7;
                        temp.Month8 = month8;
                        temp.Month9 = month9;
                        temp.Month10 = month10;
                        temp.Month11 = month11;
                        temp.Month12 = month12;
                        temp.Sum = sum;
                        break;
                    }
                }

                sumMonth1 += month1;
                sumMonth2 += month2;
                sumMonth3 += month3;
                sumMonth4 += month4;
                sumMonth5 += month5;
                sumMonth6 += month6;
                sumMonth7 += month7;
                sumMonth8 += month8;
                sumMonth9 += month9;
                sumMonth10 += month10;
                sumMonth11 += month11;
                sumMonth12 += month12;
                sumSum += sum;

            }


            foreach (var temp in mMFees)
            {
                if (temp.Department == "合計" && temp.Item == "合計")
                {
                    temp.Month1 = sumMonth1;
                    temp.Month2 = sumMonth2;
                    temp.Month3 = sumMonth3;
                    temp.Month4 = sumMonth4;
                    temp.Month5 = sumMonth5;
                    temp.Month6 = sumMonth6;
                    temp.Month7 = sumMonth7;
                    temp.Month8 = sumMonth8;
                    temp.Month9 = sumMonth9;
                    temp.Month10 = sumMonth10;
                    temp.Month11 = sumMonth11;
                    temp.Month12 = sumMonth12;
                    temp.Sum = sumSum;
                    break;
                }
            }

        }

        public static bool OverwriteFee(ObservableCollection<MMFee> workFees)
        {
            if (workFees == null) return false;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            string feeName = workFees.FirstOrDefault().FeeName;
            string feeType = workFees.FirstOrDefault().FeeType;
            int year = workFees.FirstOrDefault().Year;

            var query = from temp in hQDataDataContext.MMFees
                        where temp.FeeName == feeName && temp.Year == year && temp.FeeType == feeType
                        select temp;

            try
            {
                foreach (var workFee in workFees)
                    if (!workFee.Department.Contains("合計"))
                        hQDataDataContext.MMFees.InsertOnSubmit(workFee);

                foreach (var workFee in query)
                    hQDataDataContext.MMFees.DeleteOnSubmit(workFee);

                hQDataDataContext.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion


        #region MMFeeMaster

        public static void RefMMFeeByFeeMaster(ref MMFee mMFee)
        {
            if(mMFee == null)
            {
                return;
            }

            string feeName = mMFee.FeeName;

            string item = mMFee.Item;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var query = hQDataDataContext.MMFeeMasters.Where(x => x.FeeName == feeName && x.Item == item);

            if(query.Count() == 0)
            {
                InsertFeeMaster(new MMFeeMaster()
                {
                    FeeName = mMFee.FeeName,
                    Item = mMFee.Item,
                    Field1 = mMFee.Field1,
                    Field2 = mMFee.Field2,
                    Field3 = mMFee.Field3,
                    Field4 = mMFee.Field4,
                    Field5 = mMFee.Field5,
                    Field6 = mMFee.Field6,
                    Field7 = mMFee.Field7,
                    Field8 = mMFee.Field8,
                    Field9 = mMFee.Field9,
                    HQWork = true,
                    HQ = true,
                    Ota = true,
                    SDC = true,
                    IsFixedFee = false,
                    TimeFrom = new DateTime(1900,1,1),
                    TimeTo = new DateTime(1900,1,1),
                    Amount = 0
                });

                return;
            }

            MMFeeMaster mMFeeMaster = query.First();

            mMFee.Field1 = mMFeeMaster.Field1;
            mMFee.Field2 = mMFeeMaster.Field2;
            mMFee.Field3 = mMFeeMaster.Field3;
            mMFee.Field4 = mMFeeMaster.Field4;
            mMFee.Field5 = mMFeeMaster.Field5;
            mMFee.Field6 = mMFeeMaster.Field6;
            mMFee.Field7 = mMFeeMaster.Field7;
            mMFee.Field8 = mMFeeMaster.Field8;
            mMFee.Field9 = mMFeeMaster.Field9;

            if(mMFeeMaster.IsFixedFee == true)
            {
                DateTime month1 = new DateTime(mMFee.Year + 1, 1, 1);
                DateTime month2 = new DateTime(mMFee.Year + 1, 2, 1);
                DateTime month3 = new DateTime(mMFee.Year + 1, 3, 1);
                DateTime month4 = new DateTime(mMFee.Year, 4, 1);
                DateTime month5 = new DateTime(mMFee.Year, 5, 1);
                DateTime month6 = new DateTime(mMFee.Year, 6, 1);
                DateTime month7 = new DateTime(mMFee.Year, 7, 1);
                DateTime month8 = new DateTime(mMFee.Year, 8, 1);
                DateTime month9 = new DateTime(mMFee.Year, 9, 1);
                DateTime month10 = new DateTime(mMFee.Year, 10, 1);
                DateTime month11 = new DateTime(mMFee.Year, 11, 1);
                DateTime month12 = new DateTime(mMFee.Year, 12, 1);

                mMFee.Month1 = month1 <= mMFeeMaster.TimeTo && month1 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month2 = month2 <= mMFeeMaster.TimeTo && month2 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month3 = month3 <= mMFeeMaster.TimeTo && month3 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month4 = month4 <= mMFeeMaster.TimeTo && month4 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month5 = month5 <= mMFeeMaster.TimeTo && month5 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month6 = month6 <= mMFeeMaster.TimeTo && month6 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month7 = month7 <= mMFeeMaster.TimeTo && month7 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month8 = month8 <= mMFeeMaster.TimeTo && month8 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month9 = month9 <= mMFeeMaster.TimeTo && month9 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month10 = month10 <= mMFeeMaster.TimeTo && month10 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month11 = month11 <= mMFeeMaster.TimeTo && month11 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
                mMFee.Month12 = month12 <= mMFeeMaster.TimeTo && month12 >= mMFeeMaster.TimeFrom ? (int)mMFeeMaster.Amount : 0;
            }
        }

        public static ObservableCollection<MMFeeMaster> GetFeeMaster(string feeName)
        {

            if (string.IsNullOrEmpty(feeName))
            {
                return null;
            }

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            IQueryable<MMFeeMaster> query = hQDataDataContext.MMFeeMasters.Where(x => x.FeeName == feeName);

            return query == null ? null : new ObservableCollection<MMFeeMaster>(query.ToList());

        }

        public static bool InsertFeeMaster(MMFeeMaster mMFeeMaster)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                hQDataDataContext.MMFeeMasters.InsertOnSubmit(mMFeeMaster);

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateFeeMasterFixedPart(MMFeeMaster mMFeeMaster)
        {
            try
            {
                if (mMFeeMaster == null)
                {
                    return false;
                }

                using (var context = new HQDataDataContext())
                {
                    var update = context.MMFeeMasters.Single(x => x.ID == mMFeeMaster.ID);
                    update.IsFixedFee = mMFeeMaster.IsFixedFee;
                    update.TimeFrom = mMFeeMaster.TimeFrom;
                    update.TimeTo = mMFeeMaster.TimeTo;
                    update.Amount = mMFeeMaster.Amount;
                    context.SubmitChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool OverwriteFeeMaster(ObservableCollection<MMFeeMaster> mMFeeMasters, string feeName)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                var deleteValues = hQDataDataContext.MMFeeMasters.Where(x => x.FeeName == feeName);

                foreach (var temp in deleteValues)
                {
                    hQDataDataContext.MMFeeMasters.DeleteOnSubmit(temp);
                }

                hQDataDataContext.SubmitChanges();

                foreach (var temp in mMFeeMasters)
                {
                    hQDataDataContext.MMFeeMasters.InsertOnSubmit(temp);
                }

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion


        #region MMFeeStruct

        public static MMFeeStruct GetFieldName(string feeName)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();
            return hQDataDataContext.MMFeeStructs.Where(x =>x.FeeName == feeName).FirstOrDefault();
        }

        //public static bool IsFeeTypeDelectable(MMFeeStruct mMFeeStruct)
        //{
        //    //HQDataDataContext hQDataDataContext = new HQDataDataContext();
        //    //var query = (from temp in hQDataDataContext.MMFees
        //    //            where temp.FeeName == mMFeeStruct.FeeName && temp.FeeType == mMFeeStruct.FeeType
        //    //            select temp).Count();

        //    //if (query == 0)
        //    //    return true;
        //    //return false;
        //}

        public static bool IsFeeNameDelectable(MMFeeStruct mMFeeStruct)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();
            var query = (from temp in hQDataDataContext.MMFees
                        where temp.FeeName == mMFeeStruct.FeeName
                        select temp).Count();

            if (query == 0)
                return true;
            return false;

        }


        public static bool IsFeeTypeDelectable(MMFeeTypeStruct mMFeeTypeStruct)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var feeStruct = hQDataDataContext.MMFeeStructs.Where(x => x.Id == mMFeeTypeStruct.FeeId).FirstOrDefault();

            if (feeStruct == null) return true;

            var feeCount = hQDataDataContext.MMFees.Where(x => x.FeeName == feeStruct.FeeName && x.FeeType == mMFeeTypeStruct.FeeType).Count();

            if (feeCount == 0) return true;

            return false;
        }

        public static MMFeeStruct GetField(string company, string feeName)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var query = (from temp in hQDataDataContext.MMFeeStructs
                         where temp.Company == company && temp.FeeName == feeName
                         select temp).FirstOrDefault();

            if (query == null) return null;

            return query;

        }

        //public static ObservableCollection<MMFeeStruct> GetFeeType(string company, string feeName)
        //{
        //    HQDataDataContext hQDataDataContext = new HQDataDataContext();

        //    var query = (from temp in hQDataDataContext.MMFeeStructs
        //                where temp.Company == company && temp.FeeName == feeName
        //                select temp.FeeType).Distinct();

        //    if (query == null) return null;

        //    ObservableCollection<MMFeeStruct> mMFeeStructs = new ObservableCollection<MMFeeStruct>();

        //    foreach(var temp in query)
        //    {
        //        mMFeeStructs.Add((from feeStruct in hQDataDataContext.MMFeeStructs where feeStruct.FeeType == temp select feeStruct).FirstOrDefault());
        //    }

        //    if (mMFeeStructs.Count() == 0)
        //        return null;
        //    return mMFeeStructs;
        //}

        public static List<string> GetFeeNameMaster()
        {

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            return hQDataDataContext.MMFeeStructs.Select(x => x.FeeName).ToList();

        }

        public static ObservableCollection<MMFeeStruct> GetFeeName(string company)
        {

            if (string.IsNullOrEmpty(company)) return null;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var query = (from temp in hQDataDataContext.MMFeeStructs
                         where temp.Company == company
                         select temp.FeeName).Distinct();

            if (query == null) return null;

            ObservableCollection<MMFeeStruct> mMFeeStructs = new ObservableCollection<MMFeeStruct>();

            foreach (var temp in query)
            {
                mMFeeStructs.Add((from feeStruct in hQDataDataContext.MMFeeStructs where feeStruct.FeeName == temp select feeStruct).FirstOrDefault());
            }

            if (mMFeeStructs.Count() == 0)
                return null;
            return mMFeeStructs;
        }

        public static MMFeeStruct GetFeeStruct(string feeName)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            return hQDataDataContext.MMFeeStructs.Where(x => x.FeeName == feeName).FirstOrDefault();
        }

        public static bool OverWriteFeeStruct(ObservableCollection<MMFeeStruct> mMFeeStructs)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                // Insert new Value
                List<MMFeeStruct> deleteFeeStructs = new List<MMFeeStruct>();

                foreach (var temp in hQDataDataContext.MMFeeStructs)
                {
                    if (!isExistInList(temp.Id, mMFeeStructs))
                    {
                        deleteFeeStructs.Add(temp);
                    }
                }

                foreach (var temp in deleteFeeStructs)
                {
                    hQDataDataContext.MMFeeStructs.DeleteOnSubmit(temp);
                    if (!deleteFeeTypeStruct(temp.Id))
                        return false;
                }

                foreach (var temp in mMFeeStructs)
                {
                    if (temp.Id == 0)
                    {
                        temp.Company = "セイキョウ";
                        hQDataDataContext.MMFeeStructs.InsertOnSubmit(temp);
                    }
                    else
                    {
                        var modifyValue = hQDataDataContext.MMFeeStructs.Where(x => x.Id == temp.Id).First();
                        modifyValue.FeeName = temp.FeeName;
                        modifyValue.Field1 = temp.Field1;
                        modifyValue.Field2 = temp.Field2;
                        modifyValue.Field3 = temp.Field3;
                        modifyValue.Field4 = temp.Field4;
                        modifyValue.Field5 = temp.Field5;
                        modifyValue.Field6 = temp.Field6;
                        modifyValue.Field7 = temp.Field7;
                        modifyValue.Field8 = temp.Field8;
                        modifyValue.Field9 = temp.Field9;
                        modifyValue.Remark = temp.Remark;
                    }
                }

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateFeeStruct(MMFeeStruct mMFeeStruct)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                var updateItem = hQDataDataContext.MMFeeStructs.Where(x => x.Id == mMFeeStruct.Id).FirstOrDefault();

                updateItem.Field1 = mMFeeStruct.Field1;
                updateItem.Field2 = mMFeeStruct.Field2;
                updateItem.Field3 = mMFeeStruct.Field3;
                updateItem.Field4 = mMFeeStruct.Field4;
                updateItem.Field5 = mMFeeStruct.Field5;
                updateItem.Field6 = mMFeeStruct.Field6;
                updateItem.Field7 = mMFeeStruct.Field7;
                updateItem.Field8 = mMFeeStruct.Field8;
                updateItem.Field9 = mMFeeStruct.Field9;

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteFeeTypeStruct(string feeName)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                var FeeId = hQDataDataContext.MMFeeStructs.Where(x => x.FeeName == feeName).FirstOrDefault().Id;

                if (FeeId == 0) return false;

                var deleteValue = hQDataDataContext.MMFeeTypeStructs.Where(x => x.FeeId == FeeId);

                foreach (var temp in deleteValue)
                    hQDataDataContext.MMFeeTypeStructs.DeleteOnSubmit(temp);

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool OverWriteFeeTypeStruct(string feeName, ObservableCollection<MMFeeTypeStruct> mMFeeTypeStructs)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                // Get FeeID
                var feeId = hQDataDataContext.MMFeeStructs.Where(x => x.FeeName == feeName).FirstOrDefault().Id;


                if (feeId == 0) return false;

                var deleteValue = hQDataDataContext.MMFeeTypeStructs.Where(x => x.FeeId == feeId);


                // Insert new value
                foreach(var temp in mMFeeTypeStructs)
                {
                    temp.FeeId = feeId;
                    hQDataDataContext.MMFeeTypeStructs.InsertOnSubmit(temp);
                }

                // Delete All Data
                foreach(var temp in deleteValue)
                {
                    hQDataDataContext.MMFeeTypeStructs.DeleteOnSubmit(temp);
                }

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }


        static private bool deleteFeeTypeStruct(int feeId)
        {
            try
            {
                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                var deleteValues = hQDataDataContext.MMFeeTypeStructs
                    .Where(x => x.FeeId == feeId);

                foreach (var temp in deleteValues)
                {
                    hQDataDataContext.MMFeeTypeStructs.DeleteOnSubmit(temp);
                }

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }

        }

        static private bool isExistInList(int Id, ObservableCollection<MMFeeStruct> mMFeeStructs)
        {
            var count = mMFeeStructs.Where(x => x.Id == Id).Count();
            if (count == 0) return false;
            return true;
        }

        public static bool IsFeeNameUsed(string feeName)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            int count = hQDataDataContext.MMFees
                .Where(x => x.FeeName == feeName)
                .Count();

            if (count > 0) return true;
            return false;
        }

        #endregion


        #region MMSale

        public static void UpdateSumInSale(ref ObservableCollection<MMSale> mMSales)
        {
            foreach(MMSale mMSale in mMSales)
            {
                mMSale.Sum = mMSale.Month1 + mMSale.Month2 + mMSale.Month3 + mMSale.Month4 + mMSale.Month5 + mMSale.Month6 + mMSale.Month7 + mMSale.Month8 + mMSale.Month9 + mMSale.Month10 + mMSale.Month11 + mMSale.Month12;
            }
        }

        public static List<MMSale>[] GetSaleConflictValue(ObservableCollection<MMSale> mMSales, int year, string department)
        {

            HQDataDataContext hQDataDataContext = new HQDataDataContext();
            List<MMSale>[] conflictValueArr = new List<MMSale>[2];
            conflictValueArr[0] = new List<MMSale>();
            conflictValueArr[1] = new List<MMSale>();

            var databaseValues = hQDataDataContext.MMSales.Where(x => x.Department == department && x.Year == year);

            var conflictValue = from inputValue in mMSales
                                     join databaseValue in databaseValues
                                     on inputValue.Customer equals databaseValue.Customer
                                     select new { inputValue, databaseValue };

            foreach (var temp in conflictValue)
            {
                if (temp.inputValue.Month1  != temp.databaseValue.Month1  ||
                    temp.inputValue.Month2  != temp.databaseValue.Month2  ||
                    temp.inputValue.Month3  != temp.databaseValue.Month3  ||
                    temp.inputValue.Month4  != temp.databaseValue.Month4  ||
                    temp.inputValue.Month5  != temp.databaseValue.Month5  ||
                    temp.inputValue.Month6  != temp.databaseValue.Month6  ||
                    temp.inputValue.Month7  != temp.databaseValue.Month7  ||
                    temp.inputValue.Month8  != temp.databaseValue.Month8  ||
                    temp.inputValue.Month9  != temp.databaseValue.Month9  ||
                    temp.inputValue.Month10 != temp.databaseValue.Month10 ||
                    temp.inputValue.Month11 != temp.databaseValue.Month11 ||
                    temp.inputValue.Month12 != temp.databaseValue.Month12)
                {
                    conflictValueArr[0].Add(temp.inputValue);
                    conflictValueArr[1].Add(temp.databaseValue);
                }
            }

            return conflictValueArr;

        }

        public static ObservableCollection<MMSale> GetSale(int year)
        {

            if (year < 2000) return null;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            var query = hQDataDataContext.MMSales.Where(x => x.Year == year);

            if (query == null || query.Count() == 0) return null;

            ObservableCollection<MMSale> workSales = new ObservableCollection<MMSale>();

            var departments = query.Select(x => x.Department).Distinct();

            if (departments == null) return null;

            MMSale mMSaleSum = new MMSale()
            {
                Department = "合計",
                Customer = "合計",
                Year = year
            };

            foreach (string department in departments)
            {

                var saleRecords = query.Where(x => x.Department == department);

                MMSale mMSaleDepartmentSum = new MMSale()
                {
                    Department = "合計",
                    Customer = department + "合計",
                    Year = year
                };


                foreach (var item in saleRecords)
                {
                    item.Sum = item.Month1 + item.Month2 + item.Month3 + item.Month4 + item.Month5 + item.Month6 + item.Month7 + item.Month8 + item.Month9 + item.Month10 + item.Month11 + item.Month12;

                    mMSaleDepartmentSum.Month1 += item.Month1;
                    mMSaleDepartmentSum.Month2 += item.Month2;
                    mMSaleDepartmentSum.Month3 += item.Month3;
                    mMSaleDepartmentSum.Month4 += item.Month4;
                    mMSaleDepartmentSum.Month5 += item.Month5;
                    mMSaleDepartmentSum.Month6 += item.Month6;
                    mMSaleDepartmentSum.Month7 += item.Month7;
                    mMSaleDepartmentSum.Month8 += item.Month8;
                    mMSaleDepartmentSum.Month9 += item.Month9;
                    mMSaleDepartmentSum.Month10 += item.Month10;
                    mMSaleDepartmentSum.Month11 += item.Month11;
                    mMSaleDepartmentSum.Month12 += item.Month12;
                    mMSaleDepartmentSum.Sum += item.Sum;

                    workSales.Add(item);
                }

                workSales.Add(mMSaleDepartmentSum);

                mMSaleSum.Month1 += mMSaleDepartmentSum.Month1;
                mMSaleSum.Month2 += mMSaleDepartmentSum.Month2;
                mMSaleSum.Month3 += mMSaleDepartmentSum.Month3;
                mMSaleSum.Month4 += mMSaleDepartmentSum.Month4;
                mMSaleSum.Month5 += mMSaleDepartmentSum.Month5;
                mMSaleSum.Month6 += mMSaleDepartmentSum.Month6;
                mMSaleSum.Month7 += mMSaleDepartmentSum.Month7;
                mMSaleSum.Month8 += mMSaleDepartmentSum.Month8;
                mMSaleSum.Month9 += mMSaleDepartmentSum.Month9;
                mMSaleSum.Month10 += mMSaleDepartmentSum.Month10;
                mMSaleSum.Month11 += mMSaleDepartmentSum.Month11;
                mMSaleSum.Month12 += mMSaleDepartmentSum.Month12;
                mMSaleSum.Sum += mMSaleDepartmentSum.Sum;

            }

            if (departments.Count() != 0) workSales.Add(mMSaleSum);

            return workSales;
        }

        public static void UpdateSumInSale(ObservableCollection<MMSale> mMSales)
        {
            var query = mMSales.Select(x => x.Department).Distinct();

            foreach(var department in query)
            {
                UpdateSumInSale(ref mMSales, department);
            }
        }

        public static void UpdateSumInSale(ref ObservableCollection<MMSale> mMSales, string department)
        {
            if (mMSales == null || string.IsNullOrEmpty(department))
            {
                return;
            }

            IEnumerable<MMSale> updateItems = mMSales.Where(x => x.Department == department || x.Customer.Contains(department));

            MMSale mMSaleDepartmentSum = new MMSale();
            MMSale mMSaleSum = new MMSale();


            foreach(MMSale mMSale in updateItems)
            {
                if(mMSale.Department == department)
                {
                    mMSale.Sum = mMSale.Month1 + mMSale.Month2 + mMSale.Month3 + mMSale.Month4 + mMSale.Month5 + mMSale.Month6 + mMSale.Month7 + mMSale.Month8 + mMSale.Month9 + mMSale.Month10 + mMSale.Month11 + mMSale.Month12;

                    mMSaleDepartmentSum.Month1 += mMSale.Month1;
                    mMSaleDepartmentSum.Month2 += mMSale.Month2;
                    mMSaleDepartmentSum.Month3 += mMSale.Month3;
                    mMSaleDepartmentSum.Month4 += mMSale.Month4;
                    mMSaleDepartmentSum.Month5 += mMSale.Month5;
                    mMSaleDepartmentSum.Month6 += mMSale.Month6;
                    mMSaleDepartmentSum.Month7 += mMSale.Month7;
                    mMSaleDepartmentSum.Month8 += mMSale.Month8;
                    mMSaleDepartmentSum.Month9 += mMSale.Month9;
                    mMSaleDepartmentSum.Month10 += mMSale.Month10;
                    mMSaleDepartmentSum.Month11 += mMSale.Month11;
                    mMSaleDepartmentSum.Month12 += mMSale.Month12;
                    mMSaleDepartmentSum.Sum += mMSale.Sum;
                }
                else
                {
                    mMSaleSum = mMSale;
                }
            }

            mMSaleSum.Month1 = mMSaleDepartmentSum.Month1;
            mMSaleSum.Month2 = mMSaleDepartmentSum.Month2;
            mMSaleSum.Month3 = mMSaleDepartmentSum.Month3;
            mMSaleSum.Month4 = mMSaleDepartmentSum.Month4;
            mMSaleSum.Month5 = mMSaleDepartmentSum.Month5;
            mMSaleSum.Month6 = mMSaleDepartmentSum.Month6;
            mMSaleSum.Month7 = mMSaleDepartmentSum.Month7;
            mMSaleSum.Month8 = mMSaleDepartmentSum.Month8;
            mMSaleSum.Month9 = mMSaleDepartmentSum.Month9;
            mMSaleSum.Month10 = mMSaleDepartmentSum.Month10;
            mMSaleSum.Month11 = mMSaleDepartmentSum.Month11;
            mMSaleSum.Month12 = mMSaleDepartmentSum.Month12;
            mMSaleSum.Sum = mMSaleDepartmentSum.Sum;

            updateMainSum(ref mMSales);

        }

        private static void updateMainSum(ref ObservableCollection<MMSale> mMSales)
        {
            MMSale mMSaleMainSum = new MMSale();
            MMSale mMSaleSum = new MMSale();

            IEnumerable<MMSale> updateItems = mMSales.Where(x => x.Department.Contains("合計"));

            foreach(MMSale mMSale in updateItems)
            {

                // Get Sum item to change value
                if (mMSale.Customer == "合計")
                {
                    mMSaleMainSum = mMSale;
                }
                else
                {
                    mMSaleSum.Month1 += mMSale.Month1;
                    mMSaleSum.Month2 += mMSale.Month2;
                    mMSaleSum.Month3 += mMSale.Month3;
                    mMSaleSum.Month4 += mMSale.Month4;
                    mMSaleSum.Month5 += mMSale.Month5;
                    mMSaleSum.Month6 += mMSale.Month6;
                    mMSaleSum.Month7 += mMSale.Month7;
                    mMSaleSum.Month8 += mMSale.Month8;
                    mMSaleSum.Month9 += mMSale.Month9;
                    mMSaleSum.Month10 += mMSale.Month10;
                    mMSaleSum.Month11 += mMSale.Month11;
                    mMSaleSum.Month12 += mMSale.Month12;
                    mMSaleSum.Sum += mMSale.Sum;
                }
            }

            mMSaleMainSum.Month1 = mMSaleSum.Month1;
            mMSaleMainSum.Month2 = mMSaleSum.Month2;
            mMSaleMainSum.Month3 = mMSaleSum.Month3;
            mMSaleMainSum.Month4 = mMSaleSum.Month4;
            mMSaleMainSum.Month5 = mMSaleSum.Month5;
            mMSaleMainSum.Month6 = mMSaleSum.Month6;
            mMSaleMainSum.Month7 = mMSaleSum.Month7;
            mMSaleMainSum.Month8 = mMSaleSum.Month8;
            mMSaleMainSum.Month9 = mMSaleSum.Month9;
            mMSaleMainSum.Month10 = mMSaleSum.Month10;
            mMSaleMainSum.Month11 = mMSaleSum.Month11;
            mMSaleMainSum.Month12 = mMSaleSum.Month12;
            mMSaleMainSum.Sum = mMSaleSum.Sum;

        }

        public static bool OverwriteSale(ObservableCollection<MMSale> workSales)
        {
            if (workSales == null) return false;

            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            int year = workSales.FirstOrDefault().Year;

            var deleteWorkSales = hQDataDataContext.MMSales.Where(x => x.Year == year);

            try
            {
                foreach (var workSale in workSales)
                    if (!workSale.Department.Contains("合計"))
                        hQDataDataContext.MMSales.InsertOnSubmit(workSale);

                foreach (var workSale in deleteWorkSales)
                    hQDataDataContext.MMSales.DeleteOnSubmit(workSale);

                hQDataDataContext.SubmitChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static ObservableCollection<MMSale> GetHQMMSale(int year)
        {
            return ConvertHQQuotationSyntheticToMMSale(GetHQQuotationSynthetic(year), year);
        }

        public static List<QuotationSynthetic> GetHQQuotationSynthetic(int year)
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            DateTime timeFrom = new DateTime(year, 4, 1);
            DateTime timeTo = new DateTime(year + 1, 3, 31);

            IQueryable<QuotationSynthetic> temp = hQDataDataContext.QuotationSynthetics.Where(x => x.PaidTime <= timeTo && x.PaidTime >= timeFrom );

            return temp.ToList();
        }

        public static ObservableCollection<MMSale> ConvertHQQuotationSyntheticToMMSale(List<QuotationSynthetic> quotationSynthetics, int year)
        {
            var Customers = quotationSynthetics.Select(x => x.CustomerName).Distinct();

            ObservableCollection<MMSale> mMSales = new ObservableCollection<MMSale>();

            foreach(string customerName in Customers)
            {
                MMSale mMSale = new MMSale()
                {
                    Department = "工事",
                    Year = year,
                    Customer = customerName
                };

                var quotationSyntheticEachCustomer = quotationSynthetics.Where(x => x.CustomerName == customerName);

                foreach(var quotationSynthetic in quotationSyntheticEachCustomer)
                {
                    DateTime paidTime = (DateTime)quotationSynthetic.PaidTime;

                    if (paidTime.Month == 1) { mMSale.Month1 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 2) { mMSale.Month2 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 3) { mMSale.Month3 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 4) { mMSale.Month4 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 5) { mMSale.Month5 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 6) { mMSale.Month6 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 7) { mMSale.Month7 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 8) { mMSale.Month8 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 9) { mMSale.Month9 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 10) { mMSale.Month10 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 11) { mMSale.Month11 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                    if (paidTime.Month == 12) { mMSale.Month12 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
                }

                mMSale.Sum = mMSale.Month1 + mMSale.Month2 + mMSale.Month3 + mMSale.Month4 + mMSale.Month5 + mMSale.Month6 + mMSale.Month7 + mMSale.Month8 + mMSale.Month9 + mMSale.Month10 + mMSale.Month11 + mMSale.Month12;

                mMSales.Add(mMSale);

            }

            return mMSales;

        }

        public static bool SaveSale(ObservableCollection<MMSale> mMSales)
        {
            try
            {

                HQDataDataContext hQDataDataContext = new HQDataDataContext();

                foreach (MMSale mMSale in mMSales)
                {
                    var query = from temp in hQDataDataContext.MMSales
                                where temp.Department == mMSale.Department && temp.Customer == mMSale.Customer && temp.Year == mMSale.Year
                                select temp;
                    if (query.Count() == 0)
                    {
                        hQDataDataContext.MMSales.InsertOnSubmit(mMSale);
                    }
                    else
                    {
                        foreach (var temp in query)
                        {
                            temp.Month1 = mMSale.Month1;
                            temp.Month2 = mMSale.Month2;
                            temp.Month3 = mMSale.Month3;
                            temp.Month4 = mMSale.Month4;
                            temp.Month5 = mMSale.Month5;
                            temp.Month6 = mMSale.Month6;
                            temp.Month7 = mMSale.Month7;
                            temp.Month8 = mMSale.Month8;
                            temp.Month9 = mMSale.Month9;
                            temp.Month10 = mMSale.Month10;
                            temp.Month11 = mMSale.Month11;
                            temp.Month12 = mMSale.Month12;
                            temp.Sum = temp.Month1 + temp.Month2 + temp.Month3 + temp.Month4 + temp.Month5 + temp.Month6 + temp.Month7 + temp.Month8 + temp.Month9 + temp.Month10 + temp.Month11 + temp.Month12;
                        }
                    }
                }

                hQDataDataContext.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Ota
        //public static List<MMSale> GetOtaMMSale(int year)
        //{
        //    return ConvertOtaQuotationSyntheticToMMSale(GetOtaQuotationSynthetic(year), year);
        //}

        //public static List<MMOtaQuotationSynthetic> GetOtaQuotationSynthetic(int year)
        //{
        //    HQDataDataContext hQDataDataContext = new HQDataDataContext();

        //    List<MMOtaQuotationSynthetic> mMOtaQuotationSynthetics = new List<MMOtaQuotationSynthetic>();

        //    foreach (MMOtaQuotationSynthetic quotationSynthetic in hQDataDataContext.MMOtaQuotationSynthetics)
        //    {
        //        if (quotationSynthetic.OrderTime != null )
        //        {
        //            DateTime madeTime = (DateTime)quotationSynthetic.OrderTime;

        //            if ((madeTime.Year == year && madeTime.Month > 3) || (madeTime.Year == year + 1 && madeTime.Month < 4))
        //            {
        //                mMOtaQuotationSynthetics.Add(quotationSynthetic);
        //            }
        //        }
        //    }

        //    return mMOtaQuotationSynthetics;
        //}

        //public static List<MMSale> ConvertOtaQuotationSyntheticToMMSale(List<MMOtaQuotationSynthetic> quotationSynthetics, int year)
        //{
        //    var Customers = quotationSynthetics.Select(x => x.CustomerName).Distinct();

        //    List<MMSale> mMSales = new List<MMSale>();

        //    foreach(string customerName in Customers)
        //    {
        //        MMSale mMSale = new MMSale()
        //        {
        //            Department = "太田",
        //            Year = year,
        //            Customer = customerName
        //        };

        //        var quotationSyntheticEachCustomer = quotationSynthetics.Where(x => x.CustomerName == customerName);

        //        foreach(var quotationSynthetic in quotationSyntheticEachCustomer)
        //        {
        //            DateTime madeTime = (DateTime)quotationSynthetic.OrderTime;

        //            if (madeTime.Month == 1) { mMSale.Month1 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 2) { mMSale.Month2 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 3) { mMSale.Month3 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 4) { mMSale.Month4 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 5) { mMSale.Month5 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 6) { mMSale.Month6 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 7) { mMSale.Month7 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 8) { mMSale.Month8 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 9) { mMSale.Month9 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 10) { mMSale.Month10 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 11) { mMSale.Month11 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //            if (madeTime.Month == 12) { mMSale.Month12 += quotationSynthetic.AmountTotal == null ? 0 : (int)quotationSynthetic.AmountTotal; }
        //        }

        //        mMSale.Sum = mMSale.Month1 + mMSale.Month2 + mMSale.Month3 + mMSale.Month4 + mMSale.Month5 + mMSale.Month6 + mMSale.Month7 + mMSale.Month8 + mMSale.Month9 + mMSale.Month10 + mMSale.Month11 + mMSale.Month12;

        //        mMSales.Add(mMSale);

        //    }

        //    return mMSales;

        //}

        #endregion

        #endregion


        #region MMRevenue

        public static ObservableCollection<MMRevenue> GetRevenue(string department, int year)
        {

            if (string.IsNullOrEmpty(department))
            {
                return null;
            }

            ObservableCollection<MMRevenue> mMRevenues = new ObservableCollection<MMRevenue>();

            HQDataDataContext hQDataDataContext = new HQDataDataContext();


            // Get Sale

            var querySale= from temp in hQDataDataContext.MMSales
                           where temp.Year == year
                           select temp;

            if (department != "全社")
            {
                querySale = querySale.Where(x => x.Department == department);
            }

            MMRevenue mMRevenueSale = new MMRevenue()
            {
                Department = "合計",
                Item = "売上合計",
                Sumary = "",
                Year = year,
                Remark = "",
            };

            foreach(MMSale mMSale in querySale)
            {
                mMRevenueSale.Month1 += mMSale.Month1;
                mMRevenueSale.Month2 += mMSale.Month2;
                mMRevenueSale.Month3 += mMSale.Month3;
                mMRevenueSale.Month4 += mMSale.Month4;
                mMRevenueSale.Month5 += mMSale.Month5;
                mMRevenueSale.Month6 += mMSale.Month6;
                mMRevenueSale.Month7 += mMSale.Month7;
                mMRevenueSale.Month8 += mMSale.Month8;
                mMRevenueSale.Month9 += mMSale.Month9;
                mMRevenueSale.Month10 += mMSale.Month10;
                mMRevenueSale.Month11 += mMSale.Month11;
                mMRevenueSale.Month12 += mMSale.Month12;
                mMRevenueSale.Sum += mMSale.Sum;
            }

            mMRevenues.Add(mMRevenueSale);

            // Revenue Sum

            MMRevenue mMRevenueTotal= new MMRevenue()
            {
                Department = "合計",
                Item = "収支合計",
                Sumary = "",
                Year = year,
                Remark = "",
                Month1 = mMRevenueSale.Month1,
                Month2 = mMRevenueSale.Month2,
                Month3 = mMRevenueSale.Month3,
                Month4 = mMRevenueSale.Month4,
                Month5 = mMRevenueSale.Month5,
                Month6 = mMRevenueSale.Month6,
                Month7 = mMRevenueSale.Month7,
                Month8 = mMRevenueSale.Month8,
                Month9 = mMRevenueSale.Month9,
                Month10 = mMRevenueSale.Month10,
                Month11 = mMRevenueSale.Month11,
                Month12 = mMRevenueSale.Month12,
                Sum = mMRevenueSale.Sum,
            };

            // Get Fee

            MMRevenue mMRevenueFeeSum = new MMRevenue()
            {
                Department = "合計",
                Item = "費用合計",
                Sumary = "",
                Year = year,
                Remark = "",
            };

            var queryFee = from temp in hQDataDataContext.MMFees
                           where temp.Year == year
                           select temp;

            if (department != "全社")
                queryFee = queryFee.Where(x => x.Department == department);

            var queryFeeName = queryFee.Select(x => x.FeeName).Distinct();

            foreach(string feeName in queryFeeName)
            {

                MMRevenue mMRevenueFee = new MMRevenue()
                {
                    Department = department,
                    Item = feeName,
                    Sumary = "",
                    Year = year,
                    Remark = "",
                };

                var queryEachFeeName = queryFee.Where(x => x.FeeName == feeName);

                foreach(var item in queryEachFeeName)
                {
                    mMRevenueFee.Month1 += item.Month1;
                    mMRevenueFee.Month2 += item.Month2;
                    mMRevenueFee.Month3 += item.Month3;
                    mMRevenueFee.Month4 += item.Month4;
                    mMRevenueFee.Month5 += item.Month5;
                    mMRevenueFee.Month6 += item.Month6;
                    mMRevenueFee.Month7 += item.Month7;
                    mMRevenueFee.Month8 += item.Month8;
                    mMRevenueFee.Month9 += item.Month9;
                    mMRevenueFee.Month10 += item.Month10;
                    mMRevenueFee.Month11 += item.Month11;
                    mMRevenueFee.Month12 += item.Month12;
                    mMRevenueFee.Sum += item.Sum;
                }

                mMRevenues.Add(mMRevenueFee);

                mMRevenueFeeSum.Month1 += mMRevenueFee.Month1;
                mMRevenueFeeSum.Month2 += mMRevenueFee.Month2;
                mMRevenueFeeSum.Month3 += mMRevenueFee.Month3;
                mMRevenueFeeSum.Month4 += mMRevenueFee.Month4;
                mMRevenueFeeSum.Month5 += mMRevenueFee.Month5;
                mMRevenueFeeSum.Month6 += mMRevenueFee.Month6;
                mMRevenueFeeSum.Month7 += mMRevenueFee.Month7;
                mMRevenueFeeSum.Month8 += mMRevenueFee.Month8;
                mMRevenueFeeSum.Month9 += mMRevenueFee.Month9;
                mMRevenueFeeSum.Month10 += mMRevenueFee.Month10;
                mMRevenueFeeSum.Month11 += mMRevenueFee.Month11;
                mMRevenueFeeSum.Month12 += mMRevenueFee.Month12;
                mMRevenueFeeSum.Sum += mMRevenueFee.Sum;

            }

            mMRevenues.Add(mMRevenueFeeSum);

            mMRevenueTotal.Month1 = mMRevenueSale.Month1 - mMRevenueFeeSum.Month1;
            mMRevenueTotal.Month2 = mMRevenueSale.Month2 - mMRevenueFeeSum.Month2;
            mMRevenueTotal.Month3 = mMRevenueSale.Month3 - mMRevenueFeeSum.Month3;
            mMRevenueTotal.Month4 = mMRevenueSale.Month4 - mMRevenueFeeSum.Month4;
            mMRevenueTotal.Month5 = mMRevenueSale.Month5 - mMRevenueFeeSum.Month5;
            mMRevenueTotal.Month6 = mMRevenueSale.Month6 - mMRevenueFeeSum.Month6;
            mMRevenueTotal.Month7 = mMRevenueSale.Month7 - mMRevenueFeeSum.Month7;
            mMRevenueTotal.Month8 = mMRevenueSale.Month8 - mMRevenueFeeSum.Month8;
            mMRevenueTotal.Month9 = mMRevenueSale.Month9 - mMRevenueFeeSum.Month9;
            mMRevenueTotal.Month10 = mMRevenueSale.Month10 - mMRevenueFeeSum.Month10;
            mMRevenueTotal.Month11 = mMRevenueSale.Month11 - mMRevenueFeeSum.Month11;
            mMRevenueTotal.Month12 = mMRevenueSale.Month12 - mMRevenueFeeSum.Month12;
            mMRevenueTotal.Sum = mMRevenueSale.Sum - mMRevenueFeeSum.Sum;

            mMRevenues.Add(mMRevenueTotal);

            if (mMRevenues.Count == 0) return null;
            return mMRevenues;

        }

        #endregion


        #region UpdateSumAllDatabase

        public static void UpdateSumAllDatabase()
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            foreach(var item in hQDataDataContext.MMFees)
            {
                item.Sum = item.Month1 + item.Month2 + item.Month3 + item.Month4 + item.Month5 + item.Month6 + item.Month7 + item.Month8 + item.Month9 + item.Month10 + item.Month11 + item.Month12;
            }

            hQDataDataContext.SubmitChanges();
        }

        public static void ChangeYear()
        {
            HQDataDataContext hQDataDataContext = new HQDataDataContext();

            foreach(var item in hQDataDataContext.MMFees)
            {
                item.Year -= 4;
            }

            foreach(var item in hQDataDataContext.MMSales)
            {
                item.Year -= 4;
            }

            hQDataDataContext.SubmitChanges();
        }

        #endregion
    }
}
