using MusicPortal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;
using System.Numerics;
using MusicPortal.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private MusicPortalContext db;
        private ArtistRepository artistRepository;
        private StyleRepository styleRepository;
        private SongRepository songRepository;
        private SaltRepository saltRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(MusicPortalContext context)
        {
            db = context;
        }

        public IArtistRepository Artists
        {
            get
            {
                if (artistRepository == null)
                    artistRepository = new ArtistRepository(db);
                return artistRepository;
            }
        }

        public IStyleRepository Styles
        {
            get
            {
                if (styleRepository == null)
                   styleRepository = new StyleRepository(db);
                return styleRepository;
            }
        }
        public ISongRepository Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }
        public ISaltRepository Salts
        {
            get
            {
                if (saltRepository == null)
                    saltRepository = new SaltRepository(db);
                return saltRepository;
            }
        }
        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }      
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
