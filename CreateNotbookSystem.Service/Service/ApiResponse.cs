namespace CreateNotbookSystem.Service.Service
{
    /// <summary>
    /// 请求返回定义类
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse(string message, bool status = false)
        {
            this.Message = message;
            this.Status = status;
        }

        public ApiResponse(bool status, object result)
        {
            this.Status = status;
            this.Result = result;
        }

        /// <summary>
        /// 后台消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public object Result { get; set; }
    }
}
