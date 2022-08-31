using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure.EntityConfigurations
{
    public class ClubEntityTypeConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> clubConfiguration)
        {
            clubConfiguration.ToTable("clubs", MatchSimulationContext.DEFAULT_SCHEMA);

            clubConfiguration.HasKey(c => c.Id);

            clubConfiguration.Ignore(c => c.DomainEvents);

            clubConfiguration
               .Property<string>(c => c.Name)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasColumnName("Name")
               .IsRequired();

            clubConfiguration
                .HasOne(c => c.DomesticLeague)
                .WithMany()
                .HasForeignKey(c => c.DomesticLeagueId)
                .OnDelete(DeleteBehavior.NoAction);

            clubConfiguration
                .Property<int>(c => c.DomesticLeagueId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("DomesticLeagueId")
                .IsRequired();

            clubConfiguration
                .Property<string>(c => c.DomesticLeagueName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("DomesticLeagueName")
                .IsRequired();

            clubConfiguration
                .HasOne(c => c.ContinentalLeague)
                .WithMany()
                .HasForeignKey(c => c.ContinentalLeagueId)
                .OnDelete(DeleteBehavior.NoAction);

            clubConfiguration
                .Property<int>(c => c.ContinentalLeagueId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ContinentalLeagueId")
                .IsRequired();

            clubConfiguration
                .Property<string>(c => c.ContinentalLeagueName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ContinentalLeagueName")
                .IsRequired();

            clubConfiguration
                .Property<int>(c => c.AttackStat)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AttackStat")
                .IsRequired();

            clubConfiguration
                .Property<int>(c => c.DefenceStat)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("DefenceStat")
                .IsRequired();

            clubConfiguration
                .Property<int>(c => c.FormStat)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("FormStat")
                .IsRequired();

            clubConfiguration
                .Property<bool>(c => c.HasActiveSimulation)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("HasActiveSimulation")
                .IsRequired();
            
            clubConfiguration
                .Property<string>(c => c.ActiveSimulationId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ActiveSimulationId")
                .IsRequired(false); // not required if Club is not linked witha Simulation

            clubConfiguration
                .Property<string>(c => c.ActiveMatchId)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ActiveMatchId")
                .IsRequired(false); // not required if Club is not linked witha Simulation

            // Seeding the table

            // GreekSuperLeague clubs
            clubConfiguration.HasData(new Club("0101", "Olympiakos", League.GreekSuperLeague.Id, League.EuropaLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0102", "Panathinaikos", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 10, 8, 10));
            clubConfiguration.HasData(new Club("0103", "AEK", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 11, 10, 10));
            clubConfiguration.HasData(new Club("0104", "PAOK", League.GreekSuperLeague.Id, League.EuropaConferenceLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0105", "Aris", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 10, 8, 10));
            clubConfiguration.HasData(new Club("0106", "Volos", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 9, 7, 10));
            clubConfiguration.HasData(new Club("0107", "PAS Giannina", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 8, 8, 10));
            clubConfiguration.HasData(new Club("0108", "OFI", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 8, 7, 10));
            clubConfiguration.HasData(new Club("0109", "Asteras Tripolis", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 8, 8, 10));
            clubConfiguration.HasData(new Club("0110", "Panaitolikos", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 7, 7, 10));
            clubConfiguration.HasData(new Club("0111", "PAS Lamia", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 7, 7, 10));
            clubConfiguration.HasData(new Club("0112", "Atromitos", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 7, 7, 10));
            clubConfiguration.HasData(new Club("0113", "Apollon Smyrnis", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 6, 8, 10));
            clubConfiguration.HasData(new Club("0114", "Ionikos", League.GreekSuperLeague.Id, League.NoContinentalLeague.Id, 7, 6, 10));


            // EnglishPremierLeague clubs
            clubConfiguration.HasData(new Club("0201", "Manchester United", League.EnglishPremierLeague.Id, League.ChampionsLeague.Id, 16, 14, 10));
            clubConfiguration.HasData(new Club("0202", "Chelsea", League.EnglishPremierLeague.Id, League.ChampionsLeague.Id, 17, 16, 10));
            clubConfiguration.HasData(new Club("0203", "Liverpool", League.EnglishPremierLeague.Id, League.ChampionsLeague.Id, 17, 15, 10));
            clubConfiguration.HasData(new Club("0204", "Arsenal", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0205", "Manchester City", League.EnglishPremierLeague.Id, League.ChampionsLeague.Id, 17, 15, 10));
            clubConfiguration.HasData(new Club("0206", "Tottenham", League.EnglishPremierLeague.Id, League.EuropaConferenceLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0207", "West Ham", League.EnglishPremierLeague.Id, League.EuropaLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0208", "Wolverhampton", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0209", "Everton", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0210", "Crystal Palace", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0211", "Leicester City", League.EnglishPremierLeague.Id, League.EuropaLeague.Id, 13, 11, 10));
            clubConfiguration.HasData(new Club("0212", "Brighton", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0213", "Aston Villa", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 10, 10, 10));
            clubConfiguration.HasData(new Club("0214", "Southampton", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 10, 10, 10));
            clubConfiguration.HasData(new Club("0215", "Brentford", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 11, 10, 10));
            clubConfiguration.HasData(new Club("0216", "Leeds United", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 11, 10, 10));
            clubConfiguration.HasData(new Club("0217", "Watford", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 10, 10, 10));
            clubConfiguration.HasData(new Club("0218", "Burnley", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 10, 9, 10));
            clubConfiguration.HasData(new Club("0219", "Newcastle United", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 10, 9, 10));
            clubConfiguration.HasData(new Club("0220", "Norwich City", League.EnglishPremierLeague.Id, League.NoContinentalLeague.Id, 10, 8, 10));


            // SpanishLaLiga clubs
            clubConfiguration.HasData(new Club("0301", "Real Madrid", League.SpanishLaLiga.Id, League.ChampionsLeague.Id, 17, 14, 10));
            clubConfiguration.HasData(new Club("0302", "Barcelona", League.SpanishLaLiga.Id, League.ChampionsLeague.Id, 16, 13, 10));
            clubConfiguration.HasData(new Club("0303", "Atletico Madrid", League.SpanishLaLiga.Id, League.ChampionsLeague.Id, 16, 14, 10));
            clubConfiguration.HasData(new Club("0304", "Sevilla", League.SpanishLaLiga.Id, League.ChampionsLeague.Id, 15, 15, 10));
            clubConfiguration.HasData(new Club("0305", "Villareal", League.SpanishLaLiga.Id, League.ChampionsLeague.Id, 13, 14, 10));
            clubConfiguration.HasData(new Club("0306", "Valencia", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 15, 11, 10));
            clubConfiguration.HasData(new Club("0307", "Real Betis", League.SpanishLaLiga.Id, League.EuropaLeague.Id, 14, 12, 10));
            clubConfiguration.HasData(new Club("0308", "Real Sociedad", League.SpanishLaLiga.Id, League.EuropaLeague.Id, 14, 14, 10));
            clubConfiguration.HasData(new Club("0309", "Athletic Bilbao", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 14, 14, 10));
            clubConfiguration.HasData(new Club("0310", "Rayo Vallecano", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0311", "Osasuna", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 12, 12, 10));
            clubConfiguration.HasData(new Club("0312", "Espanyol", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 12, 13, 10));
            clubConfiguration.HasData(new Club("0313", "Mallorca", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 11, 10, 10));
            clubConfiguration.HasData(new Club("0314", "Deportivo Alaves", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 9, 12, 10));
            clubConfiguration.HasData(new Club("0315", "Celta de Vigo", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 12, 12, 10));
            clubConfiguration.HasData(new Club("0316", "Cadiz", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0317", "Granada", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 11, 12, 10));
            clubConfiguration.HasData(new Club("0318", "Elche", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 10, 11, 10));
            clubConfiguration.HasData(new Club("0319", "Levante", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 11, 8, 10));
            clubConfiguration.HasData(new Club("0320", "Getafe", League.SpanishLaLiga.Id, League.NoContinentalLeague.Id, 9, 10, 10));


            // ItalianSerieA clubs
            clubConfiguration.HasData(new Club("0401", "Milan", League.ItalianSerieA.Id, League.ChampionsLeague.Id, 16, 14, 10));
            clubConfiguration.HasData(new Club("0402", "Internazionale", League.ItalianSerieA.Id, League.ChampionsLeague.Id, 17, 14, 10));
            clubConfiguration.HasData(new Club("0403", "Juventus", League.ItalianSerieA.Id, League.ChampionsLeague.Id, 15, 14, 10));
            clubConfiguration.HasData(new Club("0404", "Roma", League.ItalianSerieA.Id, League.EuropaConferenceLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0405", "Napoli", League.ItalianSerieA.Id, League.EuropaLeague.Id, 14, 16, 10));
            clubConfiguration.HasData(new Club("0406", "Lazio", League.ItalianSerieA.Id, League.EuropaLeague.Id, 15, 12, 10));
            clubConfiguration.HasData(new Club("0407", "Atalanta", League.ItalianSerieA.Id, League.ChampionsLeague.Id, 14, 14, 10));
            clubConfiguration.HasData(new Club("0408", "Fiorentina", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 12, 14, 10));
            clubConfiguration.HasData(new Club("0409", "Bologna", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 13, 11, 10));
            clubConfiguration.HasData(new Club("0410", "Verona", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 14, 11, 10));
            clubConfiguration.HasData(new Club("0411", "Empoli", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 13, 11, 10));
            clubConfiguration.HasData(new Club("0412", "Torino", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 12, 14, 10));
            clubConfiguration.HasData(new Club("0413", "Sassuolo", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 12, 12, 10));
            clubConfiguration.HasData(new Club("0414", "Udinese", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 12, 12, 10));
            clubConfiguration.HasData(new Club("0415", "Venezia", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 10, 12, 10));
            clubConfiguration.HasData(new Club("0416", "Spezia", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 11, 9, 10));
            clubConfiguration.HasData(new Club("0417", "Genoa", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 12, 10, 10));
            clubConfiguration.HasData(new Club("0418", "Sampdoria", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 11, 9, 10));
            clubConfiguration.HasData(new Club("0419", "Salernitana", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 8, 9, 10));
            clubConfiguration.HasData(new Club("0420", "Cagliari", League.ItalianSerieA.Id, League.NoContinentalLeague.Id, 10, 9, 10));


            // GermanBundesliga clubs
            clubConfiguration.HasData(new Club("0501", "Bayern Munich", League.GermanBundesliga.Id, League.ChampionsLeague.Id, 18, 16, 10));
            clubConfiguration.HasData(new Club("0502", "Borussia Dortmund", League.GermanBundesliga.Id, League.ChampionsLeague.Id, 17, 13, 10));
            clubConfiguration.HasData(new Club("0503", "Bayer Leverkusen", League.GermanBundesliga.Id, League.EuropaLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0504", "Leipzig", League.GermanBundesliga.Id, League.ChampionsLeague.Id, 15, 15, 10));
            clubConfiguration.HasData(new Club("0505", "Wolfsburg", League.GermanBundesliga.Id, League.ChampionsLeague.Id, 13, 14, 10));
            clubConfiguration.HasData(new Club("0506", "Borussia Monchengladbach", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 13, 13, 10));
            clubConfiguration.HasData(new Club("0507", "Freiburg", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 14, 14, 10));
            clubConfiguration.HasData(new Club("0508", "Mainz", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 13, 13, 10));
            clubConfiguration.HasData(new Club("0509", "Union Berlin", League.GermanBundesliga.Id, League.EuropaConferenceLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0510", "Hoffenheim", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0511", "Koln", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 13, 11, 10));
            clubConfiguration.HasData(new Club("0512", "Bochum", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 11, 12, 10));
            clubConfiguration.HasData(new Club("0513", "Hertha Berlin", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 12, 9, 10));
            clubConfiguration.HasData(new Club("0514", "Eintracht Frankfurt", League.GermanBundesliga.Id, League.EuropaLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0515", "Stuttgart", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0516", "Augsburg", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 10, 11, 10));
            clubConfiguration.HasData(new Club("0517", "Arminia Bielefeld", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 8, 11, 10));
            clubConfiguration.HasData(new Club("0518", "Greuther Furth", League.GermanBundesliga.Id, League.NoContinentalLeague.Id, 9, 7, 10));


            // Rest ChampionsLeague clubs
            clubConfiguration.HasData(new Club("0701", "Paris Saint Germain", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 17, 14, 10));
            clubConfiguration.HasData(new Club("0702", "Club Brugge", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0703", "Porto", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 15, 13, 10));
            clubConfiguration.HasData(new Club("0704", "Ajax", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 15, 14, 10));
            clubConfiguration.HasData(new Club("0705", "Sporting Lisbon", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0706", "Besiktas", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 12, 10, 10));
            clubConfiguration.HasData(new Club("0707", "Sheriff Tiraspol", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 8, 10, 10));
            clubConfiguration.HasData(new Club("0708", "Shakhtar Donetsk", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 13, 13, 10));
            clubConfiguration.HasData(new Club("0709", "Benfica", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0710", "Dynamo Kyiv", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0711", "Young Boys", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0712", "Salzburg", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 13, 13, 10));
            clubConfiguration.HasData(new Club("0713", "Lille", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 13, 14, 10));
            clubConfiguration.HasData(new Club("0714", "Zenit Saint Petersburg", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 13, 13, 10));
            clubConfiguration.HasData(new Club("0715", "Malmo", League.NoDomesticLeague.Id, League.ChampionsLeague.Id, 10, 10, 10));

            // Rest EuropaLeague clubs
            clubConfiguration.HasData(new Club("0801", "Lyon", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0802", "Sparta Praha", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0803", "Rangers", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0804", "Brondby", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 10, 9, 10));
            clubConfiguration.HasData(new Club("0805", "Monaco", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 14, 13, 10));
            clubConfiguration.HasData(new Club("0806", "PSV Eindhoven", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 14, 12, 10));
            clubConfiguration.HasData(new Club("0807", "Sturm Graz", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 10, 9, 10));
            clubConfiguration.HasData(new Club("0808", "Legia Warsaw", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0809", "Spartak Moscow", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 12, 10, 10));
            clubConfiguration.HasData(new Club("0810", "Fenerbahce", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 12, 10, 10));
            clubConfiguration.HasData(new Club("0811", "Antwerp", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 11, 9, 10));
            clubConfiguration.HasData(new Club("0812", "Galatasaray", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 12, 12, 10));
            clubConfiguration.HasData(new Club("0813", "Marseille", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0814", "Locomotiv Moscow", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 11, 12, 10));
            clubConfiguration.HasData(new Club("0815", "Braga", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 14, 12, 10));
            clubConfiguration.HasData(new Club("0816", "Red Star Belgrade", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 11, 12, 10));
            clubConfiguration.HasData(new Club("0817", "Midtjylland", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0818", "Ludogorets", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 10, 10, 10));
            clubConfiguration.HasData(new Club("0819", "Celtic", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 13, 11, 10));
            clubConfiguration.HasData(new Club("0820", "Ferencvarosi", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 11, 9, 10));
            clubConfiguration.HasData(new Club("0821", "Dinamo Zagreb", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0822", "Genk", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 10, 9, 10));
            clubConfiguration.HasData(new Club("0823", "Rapid Wien", League.NoDomesticLeague.Id, League.EuropaLeague.Id, 10, 9, 10));

            // Rest EuropaConferenceLeague clubs
            clubConfiguration.HasData(new Club("0901", "Maccabi Tel Aviv", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 13, 11, 10));
            clubConfiguration.HasData(new Club("0902", "LASK Linz", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0903", "Helsinki", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 10, 8, 10));
            clubConfiguration.HasData(new Club("0904", "Alashkert", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 8, 7, 10));
            clubConfiguration.HasData(new Club("0905", "Gent", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 11, 12, 10));
            clubConfiguration.HasData(new Club("0906", "Partizan", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 11, 12, 10));
            clubConfiguration.HasData(new Club("0907", "Flora", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 9, 9, 10));
            clubConfiguration.HasData(new Club("0908", "Anorthosis", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 10, 9, 10));
            clubConfiguration.HasData(new Club("0909", "Bodo/Glimt", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 13, 11, 10));
            clubConfiguration.HasData(new Club("0910", "Zorya Luhansk", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 11, 11, 10));
            clubConfiguration.HasData(new Club("0911", "CSKA Sofia", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 8, 9, 10));
            clubConfiguration.HasData(new Club("0912", "AZ Alkmaar", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 12, 12, 10));
            clubConfiguration.HasData(new Club("0913", "Jablonec", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 10, 11, 10));
            clubConfiguration.HasData(new Club("0914", "Randers", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 11, 10, 10));
            clubConfiguration.HasData(new Club("0915", "Cluj", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 9, 10, 10));
            clubConfiguration.HasData(new Club("0916", "Feyenord", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 13, 13, 10));
            clubConfiguration.HasData(new Club("0917", "Slavia Praha", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 12, 10, 10));
            clubConfiguration.HasData(new Club("0918", "Maccabi Haifa", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 9, 11, 10));
            clubConfiguration.HasData(new Club("0919", "Copenhagen", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0920", "Slovan Bratislava", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 10, 10, 10));
            clubConfiguration.HasData(new Club("0921", "Lincoln Red Imps", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 8, 7, 10));
            clubConfiguration.HasData(new Club("0922", "Stade Rennais", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 13, 12, 10));
            clubConfiguration.HasData(new Club("0923", "Vitesse", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 11, 10, 10));
            clubConfiguration.HasData(new Club("0924", "Mura", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 8, 8, 10));
            clubConfiguration.HasData(new Club("0925", "Qarabag", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 11, 10, 10));
            clubConfiguration.HasData(new Club("0926", "Basel", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 12, 11, 10));
            clubConfiguration.HasData(new Club("0927", "Omonoia", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 10, 9, 10));
            clubConfiguration.HasData(new Club("0928", "Kairat Almaty", League.NoDomesticLeague.Id, League.EuropaConferenceLeague.Id, 9, 8, 10));

            // will add more
        }
    }
}
