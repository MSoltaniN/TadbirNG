using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Tests
{
    public class OptionsBuilder
    {
        public OptionsBuilder()
        {
            _gridOptions = new GridOptions()
            {
                ListChanged = false,
                Operation = 1,
                Paging = new GridPaging(),
            };
        }

        public OptionsBuilder WithBranchFilter(int branchId)
        {
            var branchFilter = new GridFilter()
            {
                FieldName = "BranchId",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsEqualTo,
                Value = branchId.ToString()
            };
            AddFilter(branchFilter);
            return this;
        }

        public OptionsBuilder UseCheckedVouchers()
        {
            var statusFilter = new GridFilter()
            {
                FieldName = "VoucherStatusId",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsGreaterOrEqualTo,
                Value = "2"
            };
            AddFilter(statusFilter);
            return this;
        }

        public OptionsBuilder UseFinalizedVouchers()
        {
            var statusFilter = new GridFilter()
            {
                FieldName = "VoucherStatusId",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsEqualTo,
                Value = "3"
            };
            AddFilter(statusFilter);
            return this;
        }

        public OptionsBuilder UseConfirmedVouchers()
        {
            var statusFilter = new GridFilter()
            {
                FieldName = "VoucherConfirmedById",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsNotNull,
            };
            AddFilter(statusFilter);
            return this;
        }

        public OptionsBuilder UseApprovedVouchers()
        {
            var statusFilter = new GridFilter()
            {
                FieldName = "VoucherApprovedById",
                FieldTypeName = "System.Int32",
                Operator = GridFilterOperator.IsNotNull,
            };
            AddFilter(statusFilter);
            return this;
        }

        public OptionsBuilder UseMarkedLines()
        {
            var markFilter = new GridFilter()
            {
                FieldName = "Mark",
                FieldTypeName = "System.String",
                Operator = GridFilterOperator.IsNotNull,
            };
            AddFilter(markFilter);
            markFilter = new GridFilter()
            {
                FieldName = "Mark",
                FieldTypeName = "System.String",
                Operator = GridFilterOperator.IsNotEqualTo,
                Value = "\"\""
            };
            AddFilter(markFilter);
            return this;
        }

        public OptionsBuilder UseUnmarkedLines()
        {
            var leftFilter = new GridFilter()
            {
                FieldName = "Mark",
                FieldTypeName = "System.String",
                Operator = GridFilterOperator.IsNull,
            };
            ////var rightFilter = new GridFilter()
            ////{
            ////    FieldName = "Mark",
            ////    FieldTypeName = "System.String",
            ////    Operator = GridFilterOperator.IsEqualTo,
            ////    Value = "\"\""
            ////};
            AddFilter(leftFilter);
            return this;
        }

        public GridOptions Build()
        {
            _filterBuilder = _filterBuilder ?? new FilterExpressionBuilder();
            _gridOptions.QuickFilter = _filterBuilder.Build();
            return _gridOptions;
        }

        private void AddFilter(GridFilter filter)
        {
            if (_filterBuilder == null)
            {
                _filterBuilder = new FilterExpressionBuilder()
                    .New(filter);
            }
            else
            {
                _filterBuilder = _filterBuilder
                    .And(filter);
            }
        }

        private void AddFiltersWithOr(GridFilter left, GridFilter right)
        {
            if (_filterBuilder == null)
            {
                _filterBuilder = new FilterExpressionBuilder()
                    .New(left)
                    .Or(right);
            }
            else
            {
                _filterBuilder = _filterBuilder
                    .And(left)
                    .Or(right);
            }
        }

        private readonly GridOptions _gridOptions;
        private FilterExpressionBuilder _filterBuilder = null;
    }
}
