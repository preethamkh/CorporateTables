using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iMISContact
/// </summary>
public class iMISContact
{
    private string contactIDField;
        
        private string firstNameField;
        
        private string lastNameField;
        
        private string memberTypeField;
        
        /// <remarks/>
        public string ContactID {
            get {
                return this.contactIDField;
            }
            set {
                this.contactIDField = value;
            }
        }
        
        /// <remarks/>
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        public string LastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
        
        /// <remarks/>
        public string MemberType {
            get {
                return this.memberTypeField;
            }
            set {
                this.memberTypeField = value;
            }
        }

}