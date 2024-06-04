﻿namespace projRESTfulApiFitConnect.DTO
{
    public class FieldDto
    {
        public string GymName { get; set; } = null!;
        public string GymAddress { get; set; } = null!;
        public string GymTime { get; set; } = null!;
        
        public string? GymPark { get; set; }

        public string? GymTraffic { get; set; }
        //public string? ClassPhoto { get; set; }
        public int FieldPhotoId { get; set; }
        public string? FieldPhoto { get; set; } = null!;
        public string? GymDescribe { get; set; }

    }
}
