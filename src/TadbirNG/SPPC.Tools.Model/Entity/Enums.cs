using System;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Defines different storage media supported by the Metadata API.
    /// </summary>
    public enum StorageMedia
    {
        /// <summary>
        /// Unspecified data storage media
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The Extensible Markup Language (XML) data storage media
        /// </summary>
        XmlFile = 1,

        /// <summary>
        /// The JavaScript Object Notation (JSON) data storage media
        /// </summary>
        JsonFile = 2,

        /// <summary>
        /// The generic database storage media
        /// </summary>
        Database = 3
    }

    /// <summary>
    /// Defines framework-agnostic visualization controls used for displaying a property.
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// Generic single-line or multi-line control for entering data as text.
        /// </summary>
        TextBox,

        /// <summary>
        /// Generic control for displaying static data as text.
        /// </summary>
        Label,

        /// <summary>
        /// Generic clickable control that initiates an action.
        /// </summary>
        Button,

        /// <summary>
        /// Generic container of selectable items with single or multiple selecton mode.
        /// </summary>
        ListBox,

        /// <summary>
        /// Generic container of selectable items with single selection mode.
        /// </summary>
        ComboBox,

        /// <summary>
        /// Generic on/off control with independent, on-mutual toggle mode.
        /// </summary>
        CheckBox,

        /// <summary>
        /// Generic on/off control with mutually exclusive toggle mode.
        /// </summary>
        RadioButton,

        /// <summary>
        /// Generic labeled container of control items.
        /// </summary>
        GroupBox,

        /// <summary>
        /// Generic container of data with tabular (grid) display.
        /// </summary>
        DataGrid,

        /// <summary>
        /// Generic control for displaying a single image.
        /// </summary>
        PictureBox,

        /// <summary>
        /// Generic tabbed container of one or more sets of controls.
        /// </summary>
        TabControl,

        /// <summary>
        /// Generic control for displaying numerical values with support for stepped increment/decrement of value.
        /// </summary>
        SpinButton,

        /// <summary>
        /// Generic control for selecting date, time or date and time values.
        /// </summary>
        DatePicker,

        /// <summary>
        /// Generic control for displaying a calendar.
        /// </summary>
        Calendar
    }

    /// <summary>
    /// Defines different types of validation rules that can be associated with an entity.
    /// </summary>
    public enum ValidationRuleType
    {
        /// <summary>
        /// Validation rule for checking value of a property.
        /// </summary>
        Value,

        /// <summary>
        /// Validation rule for checking length of a string property.
        /// </summary>
        Length,

        /// <summary>
        /// Validation rule for parsing the value of a property from a string.
        /// </summary>
        Format
    }

    /// <summary>
    /// Defines the type of association between two related entities. The values mirror classical relationships
    /// in a typical relational database.
    /// </summary>
    public enum RelationMultiplicity
    {
        /// <summary>
        /// Multiplicity of the relation is not defined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// One instance of the entity can be associated with zero or more instances of another entity
        /// (one-to-many relationship)
        /// </summary>
        OneToMany = 1,

        /// <summary>
        /// Many instances of the entity can be associated to a single instance of another entity
        /// (inverse case of a one-to-many relationship)
        /// </summary>
        ManyToOne = 2,

        /// <summary>
        /// One instance of the entity can be associated with a single instances of another entity
        /// (one-to-one relationship)
        /// </summary>
        OneToOne = 3,

        /// <summary>
        /// Many instances of the entity can be associated to many instances of another entity
        /// (many-to-many relationship)
        /// </summary>
        ManyToMany = 4
    }

    /// <summary>
    /// Provides common constant values used throughout the platform.
    /// </summary>
    public class EntityConstants
    {
        /// <summary>
        /// Gets minimum date value suitable for database operations using SQL Server.
        /// </summary>
        public static string MinDate
        {
            get { return "1900-01-01"; }
        }

        /// <summary>
        /// Gets the default date time format override when default system format will not be used.
        /// </summary>
        public static string DateFormat
        {
            get { return "yyyy-MM-dd"; }
        }
    }
}
