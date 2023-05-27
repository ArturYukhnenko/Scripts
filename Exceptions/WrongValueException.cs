using System;

namespace Exceptions {
    public class WrongValueException : Exception {
        
        public WrongValueException() {
        }

        public WrongValueException(string message) : base(message) {
        }
        
    }
}