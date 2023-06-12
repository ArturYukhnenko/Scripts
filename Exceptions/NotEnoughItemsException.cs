using System;

namespace Exceptions {
    public class NotEnoughItemsException : Exception {
        
        public NotEnoughItemsException() {
        }

        public NotEnoughItemsException(string message) : base(message) {
            
        }

    }
}