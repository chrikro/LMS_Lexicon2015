-- select U.UserName,U.GroupId as GruppId,G.Name as Gruppnamn,R.Name as Roll
-- from AspNetUsers U,AspNetUserRoles UR, AspNetRoles R, Groups G
-- where U.id = UR.UserId
-- and UR.RoleId = R.Id
-- and U.GroupId = G.Id

select U.UserName,U.GroupId as GruppId,G.Name as Gruppnamn,R.Name as Roll,
g.Id as GruppId,g.Name as Gruppnamn,g.StartDate as GruppStart, g.EndDate as GruppSlut,
C.Id as KursId,c.Name as Kursnamn, c.StartDate as Kursstart,c.EndDate as Kursslut,
a.Id as AktId,a.Name as Aktivitet,a.StartDate as AktivitetsStart,a.EndDate as AktivitetsSlut
from AspNetUsers U,AspNetUserRoles UR, AspNetRoles R,
Groups g,Activities a, CourseOccasions C
where U.id = UR.UserId
and UR.RoleId = R.Id
and U.GroupId = G.Id
and c.Id = a.CourseId
and g.Id = c.GroupId

