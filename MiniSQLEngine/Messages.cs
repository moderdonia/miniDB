using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class Messages
    {
        public const string CreateDatabaseSuccess = "Database created";
        public const string DeleteDatabaseSuccess = "Database deleted";
        public const string BackupDatabaseSuccess = "Database backed up";

        public const string CreateDatabaseError = "Error: Not sufficient privileges";

        public const string CreateTableSuccess = "Table created";
        public const string InsertSuccess = "Tuple added";
        public const string TupleDeleteSuccess = "Tuple(s) deleted";
        public const string TupleUpdateSuccess = "Tuple(s) updated";
        public const string DeleteTableSuccess =  "Table deleted";

        public const string SecurityProfileCreated = "Security profile created";
        public const string SecurityUserCreated = "Security user created";
        public const string SecurityProfileDeleted = "Security profile deleted";
        public const string SecurityUserDeleted = "Security user deleted";
        public const string SecurityPrivilegeGranted = "Security privilege granted";
        public const string SecurityPrivilegeRevoked = "Security privilege revoked";
        public const string SecurityUserAdded = "User added to security profile";

        public const string Error = "ERROR: ";

        public const string WrongSyntax = Error + "Syntactical error";
        public const string DatabaseDoesNotExist = Error + "Database does not exist";
        public const string TableDoesNotExist = Error + "Table does not exist";
        public const string ColumnDoesNotExist = Error + "Column does not exist";
        public const string IncorrectDataType = Error + "Incorrect data type";
        public const string DatabaseExist = "Database is allready created";
        public const string TableExist = "Table is allready created";


        public const string SecurityIncorrectLogin = Error + "Incorrect login";
        public const string SecurityNotSufficientPrivileges = Error + "Not sufficient privileges";
        public const string SecurityProfileAlreadyExists = Error + "Security profile already exists";
        public const string SecurityUserAlreadyExists = Error + "Security user already exists";
        public const string SecurityProfileDoesNotExist = Error + "Security profile does not exist";
        public const string SecurityUserDoesNotExist = Error + "Security user does not exist";

    }
}
