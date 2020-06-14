using System.Collections.Generic;

namespace DOTNET_RPG.Models
{
    public class Character
    {
        public int ID {get; set;}
        //Name is Frodo by Default
        public string Name{get; set;} ="Frodo";
        public int HitPoints{get; set;} = 100;
        public int Strength {get; set; }= 10; 
        public int Defense{get; set; } = 10; 
        public int Intelligence{get; set; } = 10; 
        public RpgClass Class{get; set; } = RpgClass.Knight; 
        public User user{get; set;}
        public Weapon Weapon {get; set;}
        public List<CharacterSkill> CharacterSkills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}