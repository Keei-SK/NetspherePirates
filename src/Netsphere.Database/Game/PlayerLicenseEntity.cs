﻿using LinqToDB.Mapping;

namespace Netsphere.Database.Game
{
    [Table("player_licenses")]
    public class PlayerLicenseEntity
    {
        [PrimaryKey]
        public long Id { get; set; }

        [Column]
        public long PlayerId { get; set; }

        [Column]
        public byte License { get; set; }

        [Column]
        public long FirstCompletedDate { get; set; }

        [Column]
        public int CompletedCount { get; set; }

        [Association(CanBeNull = true, ThisKey = "PlayerId", OtherKey = "Id")]
        public PlayerEntity Player { get; set; }
    }
}
