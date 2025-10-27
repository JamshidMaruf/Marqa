namespace Marqa.Domain.Entities;

public class Setting : Auditable
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Category { get; set; }
    public bool IsEncrypted { get; set; }
}

// StudentApp.AppId -> 3298423927312893
// StudentApp.SecretKey -> asd2113e^5wds12341edqwde12
// Category: App


// TeacherApp.AppId -> 3298423927312893
// TeacherApp.SecretKey -> asd2113e^5wds12341edqwde12 -> TacherApp + "asdasdqw" = hash
// ParentApp.AppId -> 3298423927312893
// ParentApp.SecretKey -> asd2113e^5wds12341edqwde12