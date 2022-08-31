using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate
{
    public class Club : Entity, IAggregateRoot
    {
        // Id

        public string Name => _name;
        private string _name;

        public League DomesticLeague { get; private set; }

        public int DomesticLeagueId => _domesticLeagueId;
        private int _domesticLeagueId;

        public string DomesticLeagueName => _domesticLeagueName;
        private string _domesticLeagueName;

        public League ContinentalLeague { get; private set; }

        public int ContinentalLeagueId => _continentalLeagueId;
        private int _continentalLeagueId;

        public string ContinentalLeagueName => _continentalLeagueName;
        private string _continentalLeagueName;

        public int AttackStat => _attackStat;
        private int _attackStat;

        public int DefenceStat => _defenceStat;
        private int _defenceStat;

        public int FormStat => _formStat;
        private int _formStat;

        public bool HasActiveSimulation => _hasActiveSimulation;
        private bool _hasActiveSimulation;
        
        public string ActiveSimulationId => _activeSimulationId;
        private string _activeSimulationId;

        public string ActiveMatchId => _activeMatchId;
        private string _activeMatchId;

        public Club(string id, string name, int domesticLeagueId, int continentalLeagueId,
                    int attackStat, int defenceStat, int formStat)
        {
            Id = id;
            _name = name;
            _domesticLeagueId = League.From(domesticLeagueId).Id;
            _domesticLeagueName = League.From(domesticLeagueId).Name;
            _continentalLeagueId = League.From(continentalLeagueId).Id;
            _continentalLeagueName = League.From(continentalLeagueId).Name;
            SetAttackStat(attackStat);
            SetDefenceStat(defenceStat);
            SetFormStat(formStat);
            _hasActiveSimulation = false;
            _activeSimulationId = String.Empty;
            _activeMatchId = String.Empty;
        }

        
        // We link (and "lock") a Club with an active Simulation to ensure that a Club cannot participate
        // simultaneously in more than one active Simulations. We also store the Id of the Simulation's related Match.
        // After the Simulation is finished, we unlink the Club.
       public void LinkWithSimulation(string simulationId, string matchId)
       {
           if (_hasActiveSimulation) throw new MatchSimulationDomainException("Club is already linked with a Simulation.");
       
           _hasActiveSimulation = true;
           _activeSimulationId = simulationId;
           _activeMatchId = matchId;
       }

        public void UnlinkFromSimulation(string simulationId)
        {
            if (!_hasActiveSimulation) throw new MatchSimulationDomainException("Club isn't linked with any Simulation.");
            
            if (!_activeSimulationId.Equals(simulationId)) 
                throw new MatchSimulationDomainException("Cannot access a Club's methods that is linked with another Simulation.");
        
            _hasActiveSimulation = false;
            _activeSimulationId = String.Empty;
            _activeMatchId = String.Empty;
        }

        public void UpdateFormAfterWin(string simulationId)
        {
            if (!_activeSimulationId.Equals(simulationId))
                throw new MatchSimulationDomainException("Cannot access a Club's methods that is linked with another Simulation.");
            
            if (FormStat == 20) return;

            // 50% chance to increase Form by 1 in normal circumstances, 90% chance if current Form <= 4
            var chance = 51;
            if (FormStat <= 4) chance = 91;
            if (RandomIntInRange(1, 100) < chance)
            {
                SetFormStat(FormStat + 1);
            }
        }

        public void UpdateFormAfterLoss(string simulationId)
        {
            if (!_activeSimulationId.Equals(simulationId))
                throw new MatchSimulationDomainException("Cannot access a Club's methods that is linked with another Simulation.");

            if (FormStat == 1) return;

            // 50% chance to decrease Form by 1 in normal circumstances, 90% chance if current Form >= 17
            var chance = 51;
            if (FormStat >= 17) chance = 91;
            if (RandomIntInRange(1, 100) < chance)
            {
                SetFormStat(FormStat - 1);
            }
        }

        private void SetAttackStat(int value)
        {
            if (value < 1 || value > 20) throw new MatchSimulationDomainException("Invalid AttackStat value.");
            _attackStat = value;
        }

        private void SetDefenceStat(int value)
        {
            if (value < 1 || value > 20) throw new MatchSimulationDomainException("Invalid DefenceStat value.");
            _defenceStat = value;
        }

        private void SetFormStat(int value)
        {
            if (value < 1 || value > 20) throw new MatchSimulationDomainException("Invalid FormStat value.");
            _formStat = value;
        }

        private int RandomIntInRange(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }
    }
}
