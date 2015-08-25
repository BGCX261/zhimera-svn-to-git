
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace Erasure
{
    class NormalChunk
    {
        public int chunk_id;
        public int chunk_size;
        public string parent_name;
        public byte[] data_part;
    }

    [Serializable]
    class EncodedChunk
    {
        
        public string parent_name;
        public List<int> chunk_ids;
        public byte[] encoded_data_part;
    }

    class FinalImpl
    {
        public List<NormalChunk> chunk_list = new List<NormalChunk>();
        public List<EncodedChunk> ec_chunk_list = new List<EncodedChunk>();
        public void BeginEncoding(string filename, int NumOutputFiles)
        {
            chunk_list = splitFile(filename, NumOutputFiles);
            Console.WriteLine("Number of normal chunks = " + chunk_list.Count);

            //verify_split(chunk_list);
            ec_chunk_list = EncodeNormalChunks(chunk_list);
            decode(ec_chunk_list, NumOutputFiles);
            manual_decoding(ec_chunk_list);
        }

        public List<NormalChunk> splitFile(string filename, int NumOutputFiles)
        {
           
            FileStream fppart = new FileStream(filename, FileMode.Open, FileAccess.Read);
            
            int fileOffset = 0;
            NormalChunk nc_obj;

            byte[] bytesource = null;
            FileInfo sourceInfo = new FileInfo(filename);

            int partsize = (int)Math.Ceiling((double)(sourceInfo.Length / NumOutputFiles));

            int sizeremaining = (int)sourceInfo.Length;

            for (int i = 0; i < NumOutputFiles; i++)
            {
                // Calculate the remaining size of the whole file
                sizeremaining = (int)sourceInfo.Length - (i * partsize);

                // The size of the last part file might differ because a file doesn't always split equally
                if (sizeremaining < partsize)
                {
                    partsize = sizeremaining;
                }
                bytesource = new byte[partsize];

                fppart.Seek(fileOffset, 0);
                fppart.Read(bytesource, 0, partsize);

                // Set the new offset
                fileOffset += partsize;

                // create NormalChunk objects
                nc_obj = new NormalChunk();
                nc_obj.chunk_id = i;
                nc_obj.chunk_size = partsize;
                nc_obj.parent_name = filename;

                nc_obj.data_part = bytesource;
                chunk_list.Add(nc_obj);

            }
            // Close the file stream
            fppart.Close();

            Console.WriteLine("Finished splitting the file");
            return chunk_list;
        }

        public void verify_split(List<NormalChunk> chunk_list)
        {
            string FolderOutputPath = "F:/LiveProjects/C#Rookie/";
            string currPath;
            Console.WriteLine("list size = " + chunk_list.Count);
            for (int i = 0; i < chunk_list.Count; i++)
            {
                currPath = FolderOutputPath + "\\" + chunk_list[i].chunk_id + ".mp3";
                
                FileStream fs = new FileStream(currPath, FileMode.Create);

                fs.Write(chunk_list[i].data_part, 0, chunk_list[i].chunk_size);
                fs.Close();
            }
        }

        public List<EncodedChunk> EncodeNormalChunks(List<NormalChunk> chunk_list)
        {
            /* using LT codes
             [1] The degree d, 1 ≤ d ≤ n, of the next packet is chosen at random.
             [2] Exactly d blocks from the message are randomly chosen.
             [3] If Mi is the ith block of the message, the data portion of the next packet is computed as
                 Mi1 ^ Mi2 ^....Mid, where {i1, i2, …, id} are the randomly chosen indices for the d blocks 
                 included in this packet.
             [4] A prefix is appended to the encoded packet, defining how many blocks n are in the message, 
                 how many blocks d have been exclusive-ored into the data portion of this packet, and the 
                 list of indices {i1, i2, …, id}.
            */

            int num_chunks = chunk_list.Count;
            int a = 0, length = 0, num_of_repeats = num_chunks + 2;                 // (num_of_repeats) signifies how many encoded 
                                                                                    // chunks need to be generated
            Console.WriteLine("Begin encoding...");
            Console.WriteLine("Num of encoded chunks that will be generated = " + num_of_repeats);
            for (int x = 0; x < num_of_repeats; x++)
            {
                Random rand = new Random(5555 + x);                                 // use a known seed            
                int degree = rand.Next(1, num_chunks);                              // degree for LT codes                            
                Console.WriteLine("degree = " + degree);
                List<int> rand_chunk_list = new List<int>();                        // array which will contain indicies of random chunk_ids
            
                EncodedChunk ec = new EncodedChunk();
                
                // generate distinct random chunks for encoding
                do
                {
                    int r = rand.Next(num_chunks);
                    if (!rand_chunk_list.Contains(r))
                    {
                        rand_chunk_list.Add(r);
                    } 
                } while (rand_chunk_list.Count < degree); 
           
                Console.Write("Chunks that need to be merged in Round " + x + " = ");
                for (int z = 0; z < degree; z++)
                {
                    Console.Write(rand_chunk_list[z] + " ");
                }
                Console.WriteLine("");
                ec.chunk_ids = new List<int>();
                for (int z = 0; z < rand_chunk_list.Count; z++)
                {
                    length = chunk_list[rand_chunk_list[z]].data_part.Length;
                    ec.encoded_data_part = new byte[length];
                    
                    while (a < length)
                    {
                        // XOr'ing
                        ec.encoded_data_part[a] = Convert.ToByte(ec.encoded_data_part[a] ^ 
                                                                 chunk_list[rand_chunk_list[z]].data_part[a]);
                        a++;
                    }
                    a = 0;
                    ec.chunk_ids.Add(rand_chunk_list[z]);
                
                }
                ec.parent_name = chunk_list[0].parent_name;

                // Now the EncodedChunk is ready. Write it to a file on hard disk
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                string encode_filename = "F:/LiveProjects/C#Rookie/EncodedChunk" + "." + String.Format(@"{0:D4}", x) + ".dat";
                Stream fp = new FileStream(encode_filename, FileMode.Create, FileAccess.ReadWrite);

                try
                {
                    binaryFormatter.Serialize(fp, ec);
                }
                catch (SerializationException se)
                {
                    Console.WriteLine(se.Message);
                }
                fp.Flush();
                fp.Close();
                ec_chunk_list.Add(ec);
                Console.WriteLine("Ending Round " + x);
            }

            return ec_chunk_list;

            /* some junk to verify...
            BinaryFormatter bin = new BinaryFormatter();
            FileStream fs = new FileStream("F:/LiveProjects/C#Rookie/EncodedChunk.0001.dat", FileMode.Open, FileAccess.ReadWrite);
            ec = (EncodedChunk)bin.Deserialize(fs);
            fs.Write(ec.encoded_data_part, 0, ec.encoded_data_part.Length);
            fs.Close();
            fs = new FileStream("F:/LiveProjects/C#Rookie/EncodedChunk.mp3", FileMode.Create, FileAccess.Write);
            fs.Write(ec.encoded_data_part, 0, (int)ec.encoded_data_part.Length);
            fs.Close();*/
            
        }

        public int getDegree(int num_chunks)
        {
            TimeSpan t = new TimeSpan();
            int r = Convert.ToInt32(t.Ticks);
            Console.WriteLine("Sec = " + r);
            Random rand = new Random();
            int random = rand.Next(num_chunks);

            if (random == 0)
                random = rand.Next(num_chunks);

            return random;
        }

        // The decode function assumes the EC's are present in a list.
        // So, there has to be a function which will extract the EC objects from 
        // hard disk and convert them into actual objects.
        // TODO: [1] - Deocde() will be running on the client side, so this function 
        //             should not take in encoded_list as its parameter. Instead, it
        //             should generate this list by itself by reading them from hard disk.
        //       [2] - Suppose if there is no EC with degree=1, then download more
        //             chunks and run the decode() algo.

        public void decode(List<EncodedChunk> encoded_list, int total_chunks)
        {
            List<EncodedChunk> encoded_list_copy = new List<EncodedChunk>(encoded_list);

            BitArray chunk_list_flag = new BitArray(total_chunks);
            int bits_set_count = 0;
            int count = 0;
            bool found_single_degree_ec = false;

            while ((bits_set_count < chunk_list.Count))
            {
                Console.WriteLine("============== While started =========================");
                ArrayList ready_chunks = new ArrayList();

                // [1] Find an EC with in-degree = 1
                for (int z = 0; z < encoded_list_copy.Count; z++)
                {
                    if (encoded_list_copy[z].chunk_ids.Count == 1)
                    {
                        found_single_degree_ec = true;
                        Console.WriteLine("EC with in-degree equals 1 = " + z);
                        ready_chunks.Add(z);
                    }
                }

                // if no EC with in-degree equals one, just halt the decoding process.
                if (!found_single_degree_ec)
                {
                    Console.WriteLine("No EC's with in-degree equal to 1");
                    return;
                }

                for (int z = 0; z < ready_chunks.Count; z++)
                {
                    Console.WriteLine("ready chunk contains = " + ready_chunks[z]);
                }

                // Now we have a list of EC indicies with in-degree equals 1,
                // from which we can extract the actual NC

                for (int z = 0; z < ready_chunks.Count; z++)
                {
                    Console.WriteLine("ready_chunks[z] = " + ready_chunks[z]);
                    int i = (int)encoded_list_copy[(int)ready_chunks[z]].chunk_ids[0];
                    Console.WriteLine("i = " + i);
                    string chunk_filename = "F:/LiveProjects/C#Rookie/NC" + (i + 1) + ".mp3";
                    FileStream fp = new FileStream(chunk_filename, FileMode.Create, FileAccess.Write);

                    //write the encoded data part of EC to this file
                    int byte_count = 0;
                    int total_bytes = (int)encoded_list_copy[(int)ready_chunks[z]].encoded_data_part.Length;
                    //int total_bytes = ec_chunk_list[(int)ready_chunks[z]].encoded_data_part.Length;
                    while (byte_count < total_bytes)
                    {
                        byte temp = new byte();
                        temp = Convert.ToByte(ec_chunk_list[(int)ready_chunks[z]].encoded_data_part[byte_count]);
                        byte_count++;
                        fp.WriteByte(temp);
                    }
                    fp.Close();
                    Console.WriteLine("writing NC" + (i + 1));

                    // set the flag only if that bit is not yet set. if already set, it means
                    // that that particular chunk has already been decoded.
                    if (chunk_list_flag.Get(i) != true)
                    {
                        chunk_list_flag.Set(i, true);

                        bits_set_count++;
                    }
                    Console.WriteLine("Num of bits set = " + bits_set_count);
                }
                for (int index = 0; index < chunk_list_flag.Count; index++)
                {
                    // if the bit is set, get the index and search for that index in encoded_list_copy
                    if (chunk_list_flag[index] == true)
                    {
                        for (int z = 0; z < encoded_list_copy.Count; z++)
                        {
                            for (int y = 0; y < encoded_list_copy[z].chunk_ids.Count; y++)
                            {
                                if ((int)encoded_list_copy[z].chunk_ids[y] == index)
                                {
                                    Console.WriteLine("inside index compare");
                                    string InputFile = "F:/LiveProjects/C#Rookie/NC" + (index + 1) + ".mp3";
                                    byte[] readbytes = System.IO.File.ReadAllBytes(InputFile);
                                    int total_byte_length = encoded_list_copy[z].encoded_data_part.Length;
                                    int temp = 0;

                                    //XOR 
                                    while (temp < total_byte_length)
                                    {
                                        encoded_list_copy[z].encoded_data_part[temp] = Convert.ToByte(encoded_list_copy[z].encoded_data_part[temp] ^ readbytes[temp]);
                                        temp++;
                                    }
                                    Console.WriteLine("Finished XOR'ing");
                                    // update meta-data
                                    encoded_list_copy[z].chunk_ids.Remove(index);
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Incrementing count");
                count++;
            }
        }

        public void manual_decoding(List<EncodedChunk> encoded_list)
        {
            Console.Out.WriteLine("Manual testing in progress...");
            /*List<EncodedChunk> encoded_list_copy = new List<EncodedChunk>(encoded_list);

            int i = 0;
            
            string chunk_filename = "F:/LiveProjects/C#Rookie/ManualNC" + (i + 1) + ".mp3";
            FileStream fp = new FileStream(chunk_filename, FileMode.Create, FileAccess.Write);

            // write the encoded data part of EC to this file
            // ready_chunks[z] = 1;
            int byte_count = 0;
            int total_bytes = (int)encoded_list_copy[1].encoded_data_part.Length;
                   
            
            while (byte_count < total_bytes)
            {
                byte temp = new byte();

                temp = Convert.ToByte(encoded_list[1].encoded_data_part[byte_count]);
                byte_count++;
                fp.WriteByte(temp);
            }
            fp.Close();
            Console.WriteLine("writing NC" + (i + 1) + " done");*/
            EncodedChunk ec = new EncodedChunk();
            BinaryFormatter bin = new BinaryFormatter();
            FileStream fs = new FileStream("F:/LiveProjects/C#Rookie/EncodedChunk.0001.dat", FileMode.Open, FileAccess.ReadWrite);
            ec = (EncodedChunk)bin.Deserialize(fs);
            fs.Write(ec.encoded_data_part, 0, ec.encoded_data_part.Length);
            fs.Close();
            fs = new FileStream("F:/LiveProjects/C#Rookie/EncodedChunk.mp3", FileMode.Create, FileAccess.Write);
            fs.Write(ec.encoded_data_part, 0, (int)ec.encoded_data_part.Length);
            fs.Close();
            


        }
    }
}
