namespace VipServices2020.Domain.Interfaces {
    public interface IAddress {
        string StreetName { get; set; }
        string StreetNumber { get; set; }
        string Town { get; set; }
    }
}