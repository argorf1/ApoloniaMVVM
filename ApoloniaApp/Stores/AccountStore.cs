using ApoloniaApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Stores
{
    public class AccountStore
    {
        private UsuarioInternoModel _currentAccount;

        public UsuarioInternoModel CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                OnCurrentAccountChanged();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentAccount = null;
        }

        private void OnCurrentAccountChanged()
        {
            CurrentAccountChanged?.Invoke();
        }

    }
}
