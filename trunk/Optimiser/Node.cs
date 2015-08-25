using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Zhimera.Optimiser.node
{
    class Node
    {
        UInt32 _ipaddr;  // 32 bit for ipaddress
        UInt32 _current_uptime;  // 32 bit for node uptime
        UInt32 _bandwidth;  // bandwidth
        UInt32 _latency;    // latency
        int _rank;       // rank of the node
        UInt32 _storage_capacity; // storage cpacity of the node
        UInt32[] _uptime_history;

        //constructors
        public Node(UInt32 ip)
        {
            _ipaddr = ip;

        }
        public Node(UInt32 ip, UInt32 utime)
        {
            _ipaddr = ip;
            _current_uptime = utime;
        }
        public Node(UInt32 ip,UInt32 bw,UInt32 latency,UInt32 storage)
        {
            _ipaddr = ip;
            _bandwidth = bw;
            _latency = latency;
            _storage_capacity = storage;
        }

        // access functions
        public UInt32 Get_ipaddr() { return _ipaddr; }
        public UInt32 Get_current_uptime() { return _current_uptime; }
        public UInt32 Get_bandwidth() { return _bandwidth; }
        public int Get_rank() { return _rank; }
        public UInt32 Get_storage_capacity() { return _storage_capacity; }
        public UInt32[] Get_uptime_history() { return _uptime_history; }

        // mutator functions
        public void Set_ipaddr(UInt32 ip) { _ipaddr = ip; }
        public void Set_current_uptime(UInt32 uptime) { _current_uptime = uptime; }
        public void Set_bandwidth(UInt32 bw) { _bandwidth = bw; }
        public void Set_latency(UInt32 latency) { _latency = latency; }
        public void Set_rank(int rank) { _rank = rank;}
        public void Set_storage_capacity(UInt32 store) { _storage_capacity = store;}
        public void Set_uptime_history(UInt32[] uptime_history) 
        {
            _uptime_history[0] = uptime_history[0];
            _uptime_history[1] = uptime_history[1];
            _uptime_history[2] = uptime_history[2];
        }

        // helper functions
        public void Generate_ipaddr()
        {
        }
        public void Compute_uptime()
        {
            // logic to get the uptime for that node
            UInt32 ut = 4;
            this.Set_current_uptime(ut);
        }
    }
}
