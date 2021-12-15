namespace ProjectOL.BL.Helpers
{
    public class Endpoints
    {
        public static string URL_BASE { get; set; } = "https://localhost:44347/";

        #region Customers CUSTOMERS
        public static string GET_CUSTOMERS { get; set; } = "api/Customers/GetAll/";
        public static string GET_CUSTOMER { get; set; } = "api/Customers/GetById/";
        public static string POST_CUSTOMERS { get; set; } = "api/Customers/";
        public static string PUT_CUSTOMERS { get; set; } = "api/Customers/";
        public static string DELETE_CUSTOMERS { get; set; } = "api/Customers/";
        #endregion

        #region Projects 
        public static string GET_PROJECTS { get; set; } = "api/Projects/GetAll/";
        public static string GET_PROJECT { get; set; } = "api/Projects/GetById/";
        public static string POST_PROJECTS { get; set; } = "api/Projects/";
        public static string PUT_PROJECTS { get; set; } = "api/Projects/";
        public static string DELETE_PROJECTS { get; set; } = "api/Projects/";
        #endregion

        #region Projects_States
        public static string GET_PROJECT_STATES { get; set; } = "api/ProjectStates/GetAll/";
        #endregion

        #region Language
        public static string GET_LANGUAGES { get; set; } = "api/Languages/GetAll/";
        #endregion

        #region ProjectLanguage
        public static string GET_PROJECT_LANGUAGES { get; set; } = "api/ProjectLanguages/GetAll/";
        public static string POST_PROJECT_LANGUAGE { get; set; } = "api/ProjectLanguages/";
        #endregion
    }
}
