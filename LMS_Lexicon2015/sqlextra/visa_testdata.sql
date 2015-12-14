--allt
select U.UserName,R.Name as Roll,U.GroupId as GruppId,G.Name as Gruppnamn,
g.StartDate as GruppStart, g.EndDate as GruppSlut,
C.Id as KursId,c.Name as Kursnamn, c.StartDate as Kursstart,c.EndDate as Kursslut,
a.Id as AktId,a.Name as Aktivitet,a.StartDate as AktivitetsStart,a.EndDate as AktivitetsSlut,
d.id as DokumentId,d.Name as DokumentNamn,d.Timestamp as Uppladdningsdatum,d.Deadline
from AspNetUsers U,AspNetUserRoles UR, AspNetRoles R,
Groups g,Activities a, CourseOccasions C,Documents D
where U.id = UR.UserId
and UR.RoleId = R.Id
and U.GroupId = G.Id
and c.Id = a.CourseId
and g.Id = c.GroupId
-- and D.GroupId = g.Id
-- and d.CourseOccasionId = c.Id
-- and d.ActivityId = a.Id

--dok
-- select * from Documents