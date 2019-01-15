﻿using System.Data;
using FluentMigrator;

namespace Netsphere.Database.Migration.Game
{
    [Migration(20190112020322)]
    public class LongIds : FluentMigrator.Migration
    {
        public override void Up()
        {
            Delete.ForeignKey()
                .FromTable("player_characters").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_deny").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_deny").ForeignColumn("DenyPlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_items").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_licenses").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_mails").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_mails").ForeignColumn("SenderPlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_settings").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");

            Alter.Table("players").AlterColumn("Id").AsInt64();
            Alter.Table("player_characters").AlterColumn("PlayerId").AsInt64();
            Alter.Table("player_deny").AlterColumn("PlayerId").AsInt64();
            Alter.Table("player_deny").AlterColumn("DenyPlayerId").AsInt64();
            Alter.Table("player_items").AlterColumn("PlayerId").AsInt64();
            Alter.Table("player_licenses").AlterColumn("PlayerId").AsInt64();
            Alter.Table("player_mails").AlterColumn("PlayerId").AsInt64();
            Alter.Table("player_mails").AlterColumn("SenderPlayerId").AsInt64();
            Alter.Table("player_settings").AlterColumn("PlayerId").AsInt64();

            Create.ForeignKey()
                .FromTable("player_characters").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_deny").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_deny").ForeignColumn("DenyPlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_items").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_licenses").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_mails").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_mails").ForeignColumn("SenderPlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_settings").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("player_characters").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_deny").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_deny").ForeignColumn("DenyPlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_items").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_licenses").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_mails").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_mails").ForeignColumn("SenderPlayerId")
                .ToTable("players").PrimaryColumn("Id");
            Delete.ForeignKey()
                .FromTable("player_settings").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id");

            Alter.Table("players").AlterColumn("Id").AsInt32();
            Alter.Table("player_characters").AlterColumn("PlayerId").AsInt32();
            Alter.Table("player_deny").AlterColumn("PlayerId").AsInt32();
            Alter.Table("player_deny").AlterColumn("DenyPlayerId").AsInt32();
            Alter.Table("player_items").AlterColumn("PlayerId").AsInt32();
            Alter.Table("player_licenses").AlterColumn("PlayerId").AsInt32();
            Alter.Table("player_mails").AlterColumn("PlayerId").AsInt32();
            Alter.Table("player_mails").AlterColumn("SenderPlayerId").AsInt32();
            Alter.Table("player_settings").AlterColumn("PlayerId").AsInt32();

            Create.ForeignKey()
                .FromTable("player_characters").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_deny").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_deny").ForeignColumn("DenyPlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_items").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_licenses").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_mails").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_mails").ForeignColumn("SenderPlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
            Create.ForeignKey()
                .FromTable("player_settings").ForeignColumn("PlayerId")
                .ToTable("players").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }
    }
}