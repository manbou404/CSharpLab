/*  [GetTimeStampBuilding]
 *
 *  Copyright (C) 2015 JFactory(manbou404)
 *
 *  This software is released under the MIT License.
 *  http://opensource.org/licenses/mit-license.php
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JFactory.CsSrcLib.Library
{
    public static class ReflectionUtils
    {
        /// <summary>アセンブリファイルのビルド日時を取得する。</summary>
        /// <param name="asm">Assembly</param>
        /// <returns>取得したビルド日時</returns>
        /// <remarks>http://sumikko8note.blog.fc2.com/blog-entry-30.html</remarks>
        public static DateTime GetTimeStampBuilding(Assembly asm)
        {
            var path = (asm != null) ? asm.Location : "";

            // ファイルオープン
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                // まずはシグネチャを探す
                byte[] signature = { 0x50, 0x45, 0x00, 0x00 };  // "PE\0\0"
                List<byte> bytes = new List<byte>();
                while (true)
                {
                    bytes.Add(br.ReadByte());

                    if (bytes.Count < signature.Length)
                    {
                        continue;
                    }

                    while (signature.Length < bytes.Count)
                    {
                        bytes.RemoveAt(0);
                    }

                    bool isMatch = true;
                    for (int i = 0; i < signature.Length; i++)
                    {
                        if (signature[i] != bytes[i])
                        {
                            isMatch = false;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        break;
                    }
                }

                // COFFファイルヘッダを読み取る
                var coff = new
                {
                    Machine = br.ReadBytes(2),
                    NumberOfSections = br.ReadBytes(2),
                    TimeDateStamp = br.ReadBytes(4),
                    PointerToSymbolTable = br.ReadBytes(4),
                    NumberOfSymbols = br.ReadBytes(4),
                    SizeOfOptionalHeader = br.ReadBytes(2),
                    Characteristics = br.ReadBytes(2),
                };

                // タイムスタンプをDateTimeに変換
                int timestamp = BitConverter.ToInt32(coff.TimeDateStamp, 0);
                DateTime baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime buildDateTimeUtc = baseDateTime.AddSeconds(timestamp);
                DateTime buildDateTimeLocal = buildDateTimeUtc.ToLocalTime();
                return buildDateTimeLocal;
            }
        }
    }
}


