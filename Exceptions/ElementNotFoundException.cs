using System;

namespace Exceptions {
    public class ElementNotFoundException : Exception {
        public ElementNotFoundException() {
        }

        public ElementNotFoundException(string message) : base(message) {
        }
    }
}