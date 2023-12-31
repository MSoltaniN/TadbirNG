﻿using System;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms
{
    public class BasicPropertyViewMapper : IPropertyViewMapper
    {
        public ViewType MapPropertyType(BuiltinType type)
        {
            var viewType = ViewType.TextBox;
            switch (type)
            {
                case BuiltinType.String:
                case BuiltinType.Char:
                case BuiltinType.Guid:
                case BuiltinType.Decimal:
                case BuiltinType.Double:
                case BuiltinType.Single:
                    viewType = ViewType.TextBox;
                    break;
                case BuiltinType.Boolean:
                    viewType = ViewType.CheckBox;
                    break;
                case BuiltinType.DateTime:
                case BuiltinType.TimeSpan:
                    viewType = ViewType.DatePicker;
                    break;
                case BuiltinType.Byte:
                case BuiltinType.Int16:
                case BuiltinType.Int32:
                case BuiltinType.Int64:
                case BuiltinType.SByte:
                case BuiltinType.UInt16:
                case BuiltinType.UInt32:
                case BuiltinType.UInt64:
                    viewType = ViewType.SpinButton;
                    break;
                default:
                    break;
            }

            return viewType;
        }

        public string GetDefaultName(string name, ViewType viewType)
        {
            string prefix = String.Empty;
            switch (viewType)
            {
                case ViewType.TextBox:
                    prefix = "txt";
                    break;
                case ViewType.Label:
                    prefix = "lbl";
                    break;
                case ViewType.Button:
                    prefix = "btn";
                    break;
                case ViewType.ListBox:
                    prefix = "lst";
                    break;
                case ViewType.ComboBox:
                    prefix = "cmb";
                    break;
                case ViewType.CheckBox:
                    prefix = "chk";
                    break;
                case ViewType.RadioButton:
                    prefix = "rad";
                    break;
                case ViewType.GroupBox:
                    prefix = "grp";
                    break;
                case ViewType.DataGrid:
                    prefix = "grd";
                    break;
                case ViewType.PictureBox:
                    prefix = "img";
                    break;
                case ViewType.TabControl:
                    prefix = "tab";
                    break;
                case ViewType.SpinButton:
                    prefix = "spn";
                    break;
                case ViewType.DatePicker:
                    prefix = "dtp";
                    break;
                case ViewType.Calendar:
                    prefix = "cal";
                    break;
                default:
                    break;
            }

            return String.Format("{0}{1}", prefix, name);
        }

        public string GetDefaultBindingMember(ViewType viewType)
        {
            var bindingMember = String.Empty;
            switch (viewType)
            {
                case ViewType.TextBox:
                case ViewType.Label:
                case ViewType.Button:
                case ViewType.GroupBox:
                    bindingMember = "Text";
                    break;
                case ViewType.CheckBox:
                case ViewType.RadioButton:
                    bindingMember = "Checked";
                    break;
                case ViewType.PictureBox:
                    bindingMember = "Image";
                    break;
                case ViewType.SpinButton:
                case ViewType.DatePicker:
                    bindingMember = "Value";
                    break;
                case ViewType.Calendar:
                case ViewType.ListBox:
                case ViewType.ComboBox:
                case ViewType.DataGrid:
                case ViewType.TabControl:
                default:
                    break;
            }

            return bindingMember;
        }
    }
}
