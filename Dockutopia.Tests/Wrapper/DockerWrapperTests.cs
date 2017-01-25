﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dockutopia.Model;
using Dockutopia.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dockutopia.Tests.Wrapper
{
    [TestClass]
    public class DockerWrapperTests
    {
        //TODO NOTE Requiers Docker. 

        [TestMethod]
        public void DockerWrapper_RunWithArgs()
        {
            //Arrange
            var command = "version";
            var exitString = string.Empty;
            var outputString = string.Empty;
            var errorString = string.Empty;


            //Act
            var wrapper = new DockerWrapper(command);

            // Run command...
            wrapper.Exited += delegate { exitString = "exit";  }; 
            wrapper.OutputDataReceived += delegate (object o, DataEventArgs e)
                {
                    outputString = "output";
                    Debug.WriteLine(e.Data);
                };
            wrapper.ErrorDataReceived += delegate { errorString = "error"; };
            wrapper.BeginRun();

     
            while (!wrapper.HasExited)
            {
                Thread.Sleep(100);
            }

            //Assert
            Assert.AreEqual("exit", exitString);
            Assert.AreEqual("output", outputString);
            Assert.AreEqual("error", errorString);


        }



    }
}