using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KachaowAuto.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPartImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "PartImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "PartImages",
                columns: new[] { "PartImageId", "ImageUrl", "PartId", "PublicId" },
                values: new object[,]
                {
                    { 1, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203068/kachaowauto/parts/rxdv9gqcsp49mzwnr8fm.jpg", 46, null },
                    { 2, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203091/kachaowauto/parts/dyaie8c7ynikcuuiiqej.jpg", 31, null },
                    { 3, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203105/kachaowauto/parts/thsbcczibnh8spsonmun.jpg", 17, null },
                    { 4, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203119/kachaowauto/parts/yithggzz3o2zkj5u9hnx.jpg", 38, null },
                    { 5, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203132/kachaowauto/parts/vndxtrtn3esyyjv7jhzf.jpg", 52, null },
                    { 6, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203146/kachaowauto/parts/ht6zvgispnz7boxh4wgu.jpg", 57, null },
                    { 7, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203160/kachaowauto/parts/sncuh48velbvyklkdq5v.jpg", 53, null },
                    { 8, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203178/kachaowauto/parts/tvmrxpwggen9jgknoslf.jpg", 18, null },
                    { 9, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203195/kachaowauto/parts/bujpr6ehhp0pbuh30ms9.webp", 35, null },
                    { 10, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203217/kachaowauto/parts/jdxc2llawr96q6ql5mdq.jpg", 36, null },
                    { 11, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203238/kachaowauto/parts/bfxmv7swtnw6w9erxcr9.jpg", 3, null },
                    { 12, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203272/kachaowauto/parts/zlts30rzbgs89u2ccud7.png", 41, null },
                    { 13, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203292/kachaowauto/parts/xt81fregkhoych98wyzp.jpg", 39, null },
                    { 14, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203308/kachaowauto/parts/qkfyhgtat87yugbepy2a.jpg", 33, null },
                    { 15, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203322/kachaowauto/parts/jeljogguchewmrbnaasz.jpg", 24, null },
                    { 16, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203343/kachaowauto/parts/nwkaczxwxkvxk5suavih.jpg", 62, null },
                    { 17, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203361/kachaowauto/parts/vumoqfreegd1hqbx0has.jpg", 13, null },
                    { 18, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203382/kachaowauto/parts/rn75chixkuglku9vpxdl.jpg", 5, null },
                    { 19, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203404/kachaowauto/parts/dmzvjdiumrrtr9gwirrg.png", 32, null },
                    { 20, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203420/kachaowauto/parts/nlrh5syefrsisrprwtjg.jpg", 6, null },
                    { 21, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203455/kachaowauto/parts/cdcnkcn66hmiahydsa6g.jpg", 42, null },
                    { 22, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203471/kachaowauto/parts/ssmujpnktrnwlumsiwdf.jpg", 47, null },
                    { 23, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203484/kachaowauto/parts/y2xwug8ei0bagohlmzih.jpg", 10, null },
                    { 24, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203501/kachaowauto/parts/ji3y6ckpqvkx0ncdeckb.jpg", 22, null },
                    { 25, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203517/kachaowauto/parts/wsdyppcwpq5qnhryzpep.jpg", 11, null },
                    { 26, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203530/kachaowauto/parts/dijymhgyehpdynxtxnf4.jpg", 65, null },
                    { 27, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203546/kachaowauto/parts/ouwvhhzwqoecfgzaqbcm.webp", 20, null },
                    { 28, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203563/kachaowauto/parts/ipfbfxodv2mzj1h4ehao.jpg", 40, null },
                    { 29, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203578/kachaowauto/parts/ytthk46e5d3cbt7b9akx.jpg", 23, null },
                    { 30, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203593/kachaowauto/parts/z8rvp2ms7ovhacwzmayy.jpg", 63, null },
                    { 31, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203612/kachaowauto/parts/ukzaersts5ipw470bi4h.jpg", 72, null },
                    { 32, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203627/kachaowauto/parts/ixprjnnvizvf4bvgnlme.jpg", 69, null },
                    { 33, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203644/kachaowauto/parts/t25efpowdpqqkxkqwood.jpg", 1, null },
                    { 34, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203689/kachaowauto/parts/y8f3sgbhfuvjfcdfpjcd.jpg", 64, null },
                    { 35, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203706/kachaowauto/parts/pdbutiihcffewgcdom4v.jpg", 66, null },
                    { 36, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203723/kachaowauto/parts/wjdtqluh76k393pbxnyn.png", 4, null },
                    { 37, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203738/kachaowauto/parts/dv2p6cqx1hg6i9qjw2tq.jpg", 51, null },
                    { 38, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203753/kachaowauto/parts/dfh1d5qsif2tkmsjkpza.jpg", 50, null },
                    { 39, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203768/kachaowauto/parts/oo44aglswf9a2cmdyj3m.jpg", 49, null },
                    { 40, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203785/kachaowauto/parts/yt3ymqwxtihnib6ifprt.webp", 54, null },
                    { 41, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203799/kachaowauto/parts/t6gcjntghjigyip1m69v.jpg", 71, null },
                    { 42, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203815/kachaowauto/parts/yenwkg39tofq4hk81grd.jpg", 14, null },
                    { 43, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203831/kachaowauto/parts/qmyxyichuxdtilwvgvlq.jpg", 45, null },
                    { 44, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203849/kachaowauto/parts/j8oiuzmfyyesv1fzug8t.jpg", 67, null },
                    { 45, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203865/kachaowauto/parts/xccjb68lzprsfwvlqeqb.jpg", 34, null },
                    { 46, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203879/kachaowauto/parts/rkj5jzvop0lwdfxiuysb.webp", 44, null },
                    { 47, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203893/kachaowauto/parts/siebcpnmhkhnymukondx.jpg", 27, null },
                    { 48, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203907/kachaowauto/parts/pdfgoupg7g27gqnhzkvg.jpg", 37, null },
                    { 49, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203919/kachaowauto/parts/qrvj9ozdxypwclm4eg1l.jpg", 21, null },
                    { 50, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203940/kachaowauto/parts/cdcpexft9oh6udsajtaj.jpg", 48, null },
                    { 51, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203953/kachaowauto/parts/g0xmmbicapjshtspmlwd.jpg", 26, null },
                    { 52, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203966/kachaowauto/parts/nfeadizk6x4hhwc2whow.jpg", 19, null },
                    { 53, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775203983/kachaowauto/parts/re5htadyrnwksvbmbqsb.jpg", 9, null },
                    { 54, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204051/kachaowauto/parts/ult6oomfcv3obgzsl3vk.jpg", 28, null },
                    { 55, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204072/kachaowauto/parts/xllkggxe8ew0kj8fithj.webp", 2, null },
                    { 56, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204090/kachaowauto/parts/cbtdb5xy5uvigtpclko0.webp", 61, null },
                    { 57, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204107/kachaowauto/parts/j5jah8qey83t6sv4gobn.jpg", 43, null },
                    { 58, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204124/kachaowauto/parts/i7m7obk1znldiuuas7ug.jpg", 55, null },
                    { 59, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204141/kachaowauto/parts/kfxp6ovjfse2nui00u0f.jpg", 56, null },
                    { 60, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204154/kachaowauto/parts/s8ntegg3rijw3jc7cll4.jpg", 8, null },
                    { 61, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204166/kachaowauto/parts/z8kqhc1wabjcib69unga.jpg", 68, null },
                    { 62, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204177/kachaowauto/parts/kaz5x9yhzdjet69tbqm0.jpg", 29, null },
                    { 63, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204190/kachaowauto/parts/owhxlxurctitks4idvyt.jpg", 7, null },
                    { 64, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204206/kachaowauto/parts/s98imtmuiemnofk76xhc.webp", 16, null },
                    { 65, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204218/kachaowauto/parts/fwohy27jwr4pyryxbulm.jpg", 15, null },
                    { 66, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204233/kachaowauto/parts/mf080slgwwvxnqpgi2pl.jpg", 25, null },
                    { 67, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204244/kachaowauto/parts/jyzsutg9byobfgdrfowt.jpg", 60, null },
                    { 68, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204257/kachaowauto/parts/uajxfhby6rmd7sbz0j1m.jpg", 59, null },
                    { 69, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204277/kachaowauto/parts/eimtbih0nu4qpnzdqmsk.png", 30, null },
                    { 70, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204292/kachaowauto/parts/rtxlaqkrt64ozn3uk7ki.jpg", 12, null },
                    { 71, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204307/kachaowauto/parts/e9pws6zcdlensddztbxz.jpg", 70, null },
                    { 72, "https://res.cloudinary.com/dlfzltmrr/image/upload/v1775204324/kachaowauto/parts/dmle30bgcgxbm5pjer9t.webp", 58, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "PartImages",
                keyColumn: "PartImageId",
                keyValue: 72);

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "PartImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
