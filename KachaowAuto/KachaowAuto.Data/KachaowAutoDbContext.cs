using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data
{
    public class KachaowAutoDbContext
	: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public KachaowAutoDbContext(DbContextOptions<KachaowAutoDbContext> options)
            : base(options)
        {
        }
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Workshop> Workshops => Set<Workshop>();

        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Model> Models => Set<Model>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<EngineType> EngineTypes => Set<EngineType>();
        public DbSet<BodyType> BodyTypes => Set<BodyType>();

        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<WorkshopService> WorkshopServices => Set<WorkshopService>();

        public DbSet<AppointmentStatus> AppointmentStatuses => Set<AppointmentStatus>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<AppointmentMechanic> AppointmentMechanics => Set<AppointmentMechanic>();

        public DbSet<PartCategory> PartCategories => Set<PartCategory>();
        public DbSet<Part> Parts => Set<Part>();
        public DbSet<PartImage> PartImages => Set<PartImage>();
        public DbSet<AppointmentPart> AppointmentParts => Set<AppointmentPart>();
        public DbSet<PartRequest> PartRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AppointmentMechanic>()
	.HasKey(am => new { am.AppointmentId, am.MechanicId });

			builder.Entity<AppointmentMechanic>()
				.HasOne(am => am.Appointment)
				.WithMany(a => a.AppointmentMechanics)
				.HasForeignKey(am => am.AppointmentId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<AppointmentMechanic>()
				.HasOne(am => am.Mechanic)
				.WithMany(u => u.AppointmentMechanics)
				.HasForeignKey(am => am.MechanicId)
				.OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PartRequest>(entity =>
            {
                entity.HasKey(pr => pr.PartRequestId);

                entity.Property(pr => pr.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(pr => pr.Note)
                    .HasMaxLength(500);

                entity.Property(pr => pr.AdminNote)
                    .HasMaxLength(500);

                entity.HasOne(pr => pr.Part)
                    .WithMany(p => p.PartRequests)
                    .HasForeignKey(pr => pr.PartId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(pr => pr.Mechanic)
                    .WithMany(u => u.PartRequests)
                    .HasForeignKey(pr => pr.MechanicId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<PartImage>().HasData(
                     new PartImage { PartImageId = 1, PartId = 46, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203068/kachaowauto/parts/rxdv9gqcsp49mzwnr8fm.jpg" },
                     new PartImage { PartImageId = 2, PartId = 31, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203091/kachaowauto/parts/dyaie8c7ynikcuuiiqej.jpg" },
                     new PartImage { PartImageId = 3, PartId = 17, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203105/kachaowauto/parts/thsbcczibnh8spsonmun.jpg" },
                     new PartImage { PartImageId = 4, PartId = 38, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203119/kachaowauto/parts/yithggzz3o2zkj5u9hnx.jpg" },
                     new PartImage { PartImageId = 5, PartId = 52, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203132/kachaowauto/parts/vndxtrtn3esyyjv7jhzf.jpg" },
                     new PartImage { PartImageId = 6, PartId = 57, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203146/kachaowauto/parts/ht6zvgispnz7boxh4wgu.jpg" },
                     new PartImage { PartImageId = 7, PartId = 53, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203160/kachaowauto/parts/sncuh48velbvyklkdq5v.jpg" },
                     new PartImage { PartImageId = 8, PartId = 18, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203178/kachaowauto/parts/tvmrxpwggen9jgknoslf.jpg" },
                     new PartImage { PartImageId = 9, PartId = 35, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203195/kachaowauto/parts/bujpr6ehhp0pbuh30ms9.webp" },
                     new PartImage { PartImageId = 10, PartId = 36, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203217/kachaowauto/parts/jdxc2llawr96q6ql5mdq.jpg" },
                     new PartImage { PartImageId = 11, PartId = 3, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203238/kachaowauto/parts/bfxmv7swtnw6w9erxcr9.jpg" },
                     new PartImage { PartImageId = 12, PartId = 41, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203272/kachaowauto/parts/zlts30rzbgs89u2ccud7.png" },
                     new PartImage { PartImageId = 13, PartId = 39, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203292/kachaowauto/parts/xt81fregkhoych98wyzp.jpg" },
                     new PartImage { PartImageId = 14, PartId = 33, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203308/kachaowauto/parts/qkfyhgtat87yugbepy2a.jpg" },
                     new PartImage { PartImageId = 15, PartId = 24, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203322/kachaowauto/parts/jeljogguchewmrbnaasz.jpg" },
                     new PartImage { PartImageId = 16, PartId = 62, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203343/kachaowauto/parts/nwkaczxwxkvxk5suavih.jpg" },
                     new PartImage { PartImageId = 17, PartId = 13, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203361/kachaowauto/parts/vumoqfreegd1hqbx0has.jpg" },
                     new PartImage { PartImageId = 18, PartId = 5, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203382/kachaowauto/parts/rn75chixkuglku9vpxdl.jpg" },
                     new PartImage { PartImageId = 19, PartId = 32, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203404/kachaowauto/parts/dmzvjdiumrrtr9gwirrg.png" },
                     new PartImage { PartImageId = 20, PartId = 6, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203420/kachaowauto/parts/nlrh5syefrsisrprwtjg.jpg" },
                     new PartImage { PartImageId = 21, PartId = 42, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203455/kachaowauto/parts/cdcnkcn66hmiahydsa6g.jpg" },
                     new PartImage { PartImageId = 22, PartId = 47, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203471/kachaowauto/parts/ssmujpnktrnwlumsiwdf.jpg" },
                     new PartImage { PartImageId = 23, PartId = 10, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203484/kachaowauto/parts/y2xwug8ei0bagohlmzih.jpg" },
                     new PartImage { PartImageId = 24, PartId = 22, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203501/kachaowauto/parts/ji3y6ckpqvkx0ncdeckb.jpg" },
                     new PartImage { PartImageId = 25, PartId = 11, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203517/kachaowauto/parts/wsdyppcwpq5qnhryzpep.jpg" },
                     new PartImage { PartImageId = 26, PartId = 65, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203530/kachaowauto/parts/dijymhgyehpdynxtxnf4.jpg" },
                     new PartImage { PartImageId = 27, PartId = 20, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203546/kachaowauto/parts/ouwvhhzwqoecfgzaqbcm.webp" },
                     new PartImage { PartImageId = 28, PartId = 40, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203563/kachaowauto/parts/ipfbfxodv2mzj1h4ehao.jpg" },
                     new PartImage { PartImageId = 29, PartId = 23, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203578/kachaowauto/parts/ytthk46e5d3cbt7b9akx.jpg" },
                     new PartImage { PartImageId = 30, PartId = 63, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203593/kachaowauto/parts/z8rvp2ms7ovhacwzmayy.jpg" },
                     new PartImage { PartImageId = 31, PartId = 72, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203612/kachaowauto/parts/ukzaersts5ipw470bi4h.jpg" },
                     new PartImage { PartImageId = 32, PartId = 69, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203627/kachaowauto/parts/ixprjnnvizvf4bvgnlme.jpg" },
                     new PartImage { PartImageId = 33, PartId = 1, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203644/kachaowauto/parts/t25efpowdpqqkxkqwood.jpg" },
                     new PartImage { PartImageId = 34, PartId = 64, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203689/kachaowauto/parts/y8f3sgbhfuvjfcdfpjcd.jpg" },
                     new PartImage { PartImageId = 35, PartId = 66, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203706/kachaowauto/parts/pdbutiihcffewgcdom4v.jpg" },
                     new PartImage { PartImageId = 36, PartId = 4, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203723/kachaowauto/parts/wjdtqluh76k393pbxnyn.png" },
                     new PartImage { PartImageId = 37, PartId = 51, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203738/kachaowauto/parts/dv2p6cqx1hg6i9qjw2tq.jpg" },
                     new PartImage { PartImageId = 38, PartId = 50, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203753/kachaowauto/parts/dfh1d5qsif2tkmsjkpza.jpg" },
                     new PartImage { PartImageId = 39, PartId = 49, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203768/kachaowauto/parts/oo44aglswf9a2cmdyj3m.jpg" },
                     new PartImage { PartImageId = 40, PartId = 54, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203785/kachaowauto/parts/yt3ymqwxtihnib6ifprt.webp" },
                     new PartImage { PartImageId = 41, PartId = 71, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203799/kachaowauto/parts/t6gcjntghjigyip1m69v.jpg" },
                     new PartImage { PartImageId = 42, PartId = 14, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203815/kachaowauto/parts/yenwkg39tofq4hk81grd.jpg" },
                     new PartImage { PartImageId = 43, PartId = 45, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203831/kachaowauto/parts/qmyxyichuxdtilwvgvlq.jpg" },
                     new PartImage { PartImageId = 44, PartId = 67, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203849/kachaowauto/parts/j8oiuzmfyyesv1fzug8t.jpg" },
                     new PartImage { PartImageId = 45, PartId = 34, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203865/kachaowauto/parts/xccjb68lzprsfwvlqeqb.jpg" },
                     new PartImage { PartImageId = 46, PartId = 44, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203879/kachaowauto/parts/rkj5jzvop0lwdfxiuysb.webp" },
                     new PartImage { PartImageId = 47, PartId = 27, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203893/kachaowauto/parts/siebcpnmhkhnymukondx.jpg" },
                     new PartImage { PartImageId = 48, PartId = 37, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203907/kachaowauto/parts/pdfgoupg7g27gqnhzkvg.jpg" },
                     new PartImage { PartImageId = 49, PartId = 21, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203919/kachaowauto/parts/qrvj9ozdxypwclm4eg1l.jpg" },
                     new PartImage { PartImageId = 50, PartId = 48, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203940/kachaowauto/parts/cdcpexft9oh6udsajtaj.jpg" },
                     new PartImage { PartImageId = 51, PartId = 26, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203953/kachaowauto/parts/g0xmmbicapjshtspmlwd.jpg" },
                     new PartImage { PartImageId = 52, PartId = 19, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203966/kachaowauto/parts/nfeadizk6x4hhwc2whow.jpg" },
                     new PartImage { PartImageId = 53, PartId = 9, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203983/kachaowauto/parts/re5htadyrnwksvbmbqsb.jpg" },
                     new PartImage { PartImageId = 54, PartId = 28, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204051/kachaowauto/parts/ult6oomfcv3obgzsl3vk.jpg" },
                     new PartImage { PartImageId = 55, PartId = 2, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204072/kachaowauto/parts/xllkggxe8ew0kj8fithj.webp" },
                     new PartImage { PartImageId = 56, PartId = 61, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204090/kachaowauto/parts/cbtdb5xy5uvigtpclko0.webp" },
                     new PartImage { PartImageId = 57, PartId = 43, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204107/kachaowauto/parts/j5jah8qey83t6sv4gobn.jpg" },
                     new PartImage { PartImageId = 58, PartId = 55, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204124/kachaowauto/parts/i7m7obk1znldiuuas7ug.jpg" },
                     new PartImage { PartImageId = 59, PartId = 56, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204141/kachaowauto/parts/kfxp6ovjfse2nui00u0f.jpg" },
                     new PartImage { PartImageId = 60, PartId = 8, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204154/kachaowauto/parts/s8ntegg3rijw3jc7cll4.jpg" },
                     new PartImage { PartImageId = 61, PartId = 68, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204166/kachaowauto/parts/z8kqhc1wabjcib69unga.jpg" },
                     new PartImage { PartImageId = 62, PartId = 29, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204177/kachaowauto/parts/kaz5x9yhzdjet69tbqm0.jpg" },
                     new PartImage { PartImageId = 63, PartId = 7, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204190/kachaowauto/parts/owhxlxurctitks4idvyt.jpg" },
                     new PartImage { PartImageId = 64, PartId = 16, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204206/kachaowauto/parts/s98imtmuiemnofk76xhc.webp" },
                     new PartImage { PartImageId = 65, PartId = 15, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204218/kachaowauto/parts/fwohy27jwr4pyryxbulm.jpg" },
                     new PartImage { PartImageId = 66, PartId = 25, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204233/kachaowauto/parts/mf080slgwwvxnqpgi2pl.jpg" },
                     new PartImage { PartImageId = 67, PartId = 60, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204244/kachaowauto/parts/jyzsutg9byobfgdrfowt.jpg" },
                     new PartImage { PartImageId = 68, PartId = 59, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204257/kachaowauto/parts/uajxfhby6rmd7sbz0j1m.jpg" },
                     new PartImage { PartImageId = 69, PartId = 30, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204277/kachaowauto/parts/eimtbih0nu4qpnzdqmsk.png" },
                     new PartImage { PartImageId = 70, PartId = 12, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204292/kachaowauto/parts/rtxlaqkrt64ozn3uk7ki.jpg" },
                     new PartImage { PartImageId = 71, PartId = 70, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204307/kachaowauto/parts/e9pws6zcdlensddztbxz.jpg" },
                     new PartImage { PartImageId = 72, PartId = 58, ImageUrl = "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204324/kachaowauto/parts/dmle30bgcgxbm5pjer9t.webp" }
            );

        }

    }
}
