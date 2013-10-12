using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver.GridFS;
using System.IO;
namespace MongoSampleTests
{
    [TestClass]
    public class MongoGridFSTests: MongoSampleTestBase
    {

        [TestInitialize]
        override public void Setup()
        {
            base.Setup();
            /*gridFs = new MongoGridFS(server, "gridfs", new MongoGridFSSettings()
            {
                { ChunkSize, 1024},
                { Root, "files"} 
 
            });*/
        }
        [TestCleanup]
        public override void TearDown()
        {
            TearDownDatabase();
            database.DropCollection("fs.chunks");
            database.DropCollection("fs.files");

        }

        [TestMethod]
        public void TestGridFSUpload()
        {
            using (var fs = new FileStream("largeVideo.m4v", FileMode.Open))
            {
                database.GridFS.Upload(fs, "largeVideo.m4v");
            }
        }

        [TestMethod]
        public void TestGridFSDownload()
        {
            // put some data there 
            TestGridFSUpload();

            // download it
            database.GridFS.Download("test.m4v", "largeVideo.m4v");

            Assert.IsTrue(File.Exists("test.m4v"));

            // clean up
            try
            {
                File.Delete("test.m4v");
            }
            catch { }
        }
    }
}
