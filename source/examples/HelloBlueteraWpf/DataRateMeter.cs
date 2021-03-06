﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloBlueteraWpf
{
    class DataRateMeter
    {
        private const double alpha = 0.1;
        private const double secondsPerTick = 1e-3;

        private uint _prevTimestamp = 0;
        private double _averageDataInterval = Double.NaN;

        public double DataRate {
            get
            {
                double dataRate = 1.0 / _averageDataInterval;
                return Double.IsNaN(dataRate) ? 0.0 : dataRate;
            }
        }

        public void Reset()
        {
            _prevTimestamp = 0;
            _averageDataInterval = Double.NaN;
        }

        public void Update(uint timestamp)
        {
            if (_prevTimestamp != 0)
            {
                double deltaTime = (timestamp - _prevTimestamp) * secondsPerTick;

                if (Double.IsNaN(_averageDataInterval))
                {
                    _averageDataInterval = deltaTime;
                }
                else
                {
                    _averageDataInterval += alpha * (deltaTime - _averageDataInterval);
                }
            }

            _prevTimestamp = timestamp;
        }
    }
}
