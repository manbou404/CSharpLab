[BoxList(T)]

Copyright (c) 2016 JFactory(manbou404)

This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php


Width / Height

FixedWidth / FixedHeight


T this[int col, int row] { get; set; }  
IEnumerable<T> Cols(int row);   
IEnumerable<T> Rows(int col);  

bool IsEmpty();  
void Clear();  

void InsertCols(int col, Func<int, int, T> getNew = null);  
void InsertRows(int row, Func<int, int, T> getNew = null);  

void RemoveCols(int col, Func<int, int, T> getNew = null);  
void RemoveRows(int row, Func<int, int, T> getNew = null);  
