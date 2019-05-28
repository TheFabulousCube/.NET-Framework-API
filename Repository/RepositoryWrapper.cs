using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper: Contracts.IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICartRepository _cart;
        private ICatagoryLookupRepository _catagoryLookup;
        private IClothingRepository _clothing;
        private IDownloadsRepository _downloads;
        private IMagnetsRepository _magnets;
        private IRoleLookupRepository _roleLookup;
        private ISizeLookupRepository _sizeLookup;
        private ISleeveLookupRepository _sleeveLookup;
        private IUsersRepository _users;

        public ICartRepository Cart
        {
            get
            {
                if(_cart == null)
                {
                    _cart = new CartRepository(_repoContext);
                }
            return _cart;
            }
        }

        public ICatagoryLookupRepository CatagoryLookup
        {
            get
            {
                if (_catagoryLookup == null)
                {
                    _catagoryLookup = new CatagoryLookupRepository(_repoContext);
                }
                return _catagoryLookup;
            }
        }

        public IClothingRepository Clothing
        {
            get
            {
                if (_clothing == null)
                {
                    _clothing = new ClothingRepository(_repoContext);
                }
                return _clothing;
            }
        }

        public IDownloadsRepository Downloads
        {
            get
            {
                if (_downloads == null)
                {
                    _downloads = new DownloadsRepository(_repoContext);
                }
                return _downloads;
            }
        }

        public IMagnetsRepository Magnets
        {
            get
            {
                if (_magnets == null)
                {
                    _magnets = new MagnetsRepository(_repoContext);
                }
                return _magnets;
            }
        }

        public IRoleLookupRepository RoleLookup
        {
            get
            {
                if (_roleLookup == null)
                {
                    _roleLookup = new RoleLookupRepository(_repoContext);
                }
                return _roleLookup;
            }
        }

        public ISizeLookupRepository SizeLookup
        {
            get
            {
                if (_sizeLookup == null)
                {
                    _sizeLookup = new SizeLookupRepository(_repoContext);
                }
                return _sizeLookup;
            }
        }

        public ISleeveLookupRepository SleeveLookup
        {
            get
            {
                if (_sleeveLookup == null)
                {
                    _sleeveLookup = new SleeveLookupRepository(_repoContext);
                }
                return _sleeveLookup;
            }
        }

        public IUsersRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UsersRepository(_repoContext);
                }
                return _users;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
