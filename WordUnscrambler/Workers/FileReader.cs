﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordUnscrambler.Workers
{
    class FileReader
    {
        public string[] Read(string fileName)
        {
            string[] fileContent;
            try
            {
                fileContent = File.ReadAllLines(fileName);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            return fileContent;
        }
    }
}
