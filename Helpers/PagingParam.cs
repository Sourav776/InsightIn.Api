﻿using System;

namespace InsightIn.Api.Helpers
{
    public class PagingParam
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        private int _page;
        private int _pageSize;
        private string _searchString;
        private string _searchStringType;
        private string _searchString2;
        public int Page
        {
            get => _page;
            set
            {
                _page = value <= 0 ? 1 : value;
            }
        }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value <= 0 ? 5 : value;
            }
        }
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value?.Trim().ToLower();
            }
        }
        public string SearchString2
        {
            get => _searchString2;
            set
            {
                _searchString2 = value?.Trim().ToLower();
            }
        }
        public string SearchStringType
        {
            get => _searchStringType;
            set
            {
                _searchStringType = value?.Trim().ToLower();
            }
        }
        public int Skip => _pageSize * (_page - 1);
    }
    public class UserParam
    {
        public string UserId { get; set; }
        public long UserLevelId { get; set; }
        public long BranchId { get; set; }
        public long WingId { get; set; }
        public long SectionId { get; set; }
        public string IsReceived { get; set; }
        public string IsReturn { get; set; }
        public long TypeId { get; set; }
        public long DesignationId { get; set; }
        public DateTime BusinessDate { get; set; }
        public DateTime BusinessDateTo { get; set; }
        public int TotalRecord { get; set; }
        public int FileCheckPending { get; set; }
        public string Archive { get; set; }
        public long OwnUserlevelId { get; set; }
        public long InvestigationCategory { get; set; }
    }
    //public class DateParam
    //{
    //    public DateTimeOffset? FromDate { get; set; }
    //    public DateTimeOffset? ToDate { get; set; }
    //}
}
