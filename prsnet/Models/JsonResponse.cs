using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prsnet.Models {

    public class JsonResponse {

        public int Code { get; set; } = 0;
        public string Message { get; set; } = "Ok";
        public object Data { get; set; } = null;
        public object Error { get; set; } = null;

        public static JsonResponse Ok(object data = null) {
            return new JsonResponse { Data = data };
        }
        public static JsonResponse IdNotFound(string entityName = "", int? idValue = 0) {
            return new JsonResponse { Code = -100, Message = $"{entityName} id {idValue} not found" };
        }
        public static JsonResponse EntityIsNull(string entityName) {
            return new JsonResponse { Code = -101, Message = $"{entityName} is null" };
        }
        public static JsonResponse ModelStateError(object error) {
            return new JsonResponse { Code = -102, Message = "Model state invalid", Error = error };
        }
        public static JsonResponse LoginFailed() {
            return new JsonResponse { Code = -103, Message = "Login failed for username/password" };
        }

        public JsonResponse() { }
    }
}