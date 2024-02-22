using FileDataBase.Types;
using System;

namespace FileDataBase.Collections;

[Serializable]
public class StatisticsCollection : FileObjectWithKeyCollection<GlobalStatistics, DateTime>
{
}
