using System;

namespace Exceptions {
    public class NotEnoughSpaceException : Exception {
        public NotEnoughSpaceException() {
        }

        public NotEnoughSpaceException(string message) : base(message) {
        }
    }
}