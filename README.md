# MonthyCardExtractFormatter

reges to use:

1. :%s/\(\d\d.\d\d.\d\d\) \(\D\)/\1;\2/g
2. :%s/ \([+_-]\d\)/;\1/g
3. :%s/ \(\d\.\)/;\1/g
4. :%s/;+\(\d\)/;\1/g
5. :%s/\(\d\),\(\d\d\)/\1.\2/g
6. :%s/;\(\d\).\(\d\d\)/;\1,\2/g

File format:
03.12.14 some asstablashemnt SI56 234 234234 432523 -1,42 1.972,06
some reasont - assf
03.12.21 some asstablashemnt SI234 3453 23423 2423 423 +4.563,54 222.231,75

