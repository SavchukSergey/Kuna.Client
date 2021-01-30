using System.Collections.Generic;
using System.Linq;

namespace Kuna.Client {
    public class UserWallets {

        public IDictionary<string, UserWallet> Wallets { get; }

        public UserWallets(IEnumerable<UserWallet> wallets) {
            Wallets = wallets.ToDictionary(wallet => wallet.Currency);
        }

    }
}