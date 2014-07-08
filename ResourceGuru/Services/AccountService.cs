using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class AccountService
    {
        protected RequestHelper _requestHelper;
        public AccountService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        /// <summary>
        /// returns an list of Active Accounts.
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAllAccounts()
        {
            string url = "/v1/accounts";
            return _requestHelper.Get<List<Account>>(url);
        }

        /// <summary>
        /// returns the specified Account.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountDetail GetAccount(int id)
        {
            string url = string.Format("/v1/accounts/{0}", id);
            return _requestHelper.Get<AccountDetail>(url);
        }
    }
}
