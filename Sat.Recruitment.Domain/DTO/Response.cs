namespace Sat.Recruitment.Domain.DTO
{
    /// <summary>
    ///   Response for user creation process
    /// </summary>
    public class Response
    {
        /// <summary>Gets or sets a value indicating whether this instance is success.</summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
        public bool IsSuccess { get; set; }
        /// <summary>Gets or sets the errors.</summary>
        /// <value>The errors.</value>
        public string Errors { get; set; }
    }
}
