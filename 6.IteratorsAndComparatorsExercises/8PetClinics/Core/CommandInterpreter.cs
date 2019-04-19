using System;
using System.Collections.Generic;
using System.Linq;

namespace _8PetClinics.Core
{
    public class CommandInterpreter
    {
        private List<Pet> pets;
        private List<Clinic> clinics;
        private Pet pet;
        private Clinic clinic;

        public CommandInterpreter()
        {
            this.pets = new List<Pet>();
            this.clinics = new List<Clinic>();
        }

        public void ExecuteCommand(string commandType, string[] commandArgs)
        {
            switch (commandType)
            {
                case "Create":
                    Create(commandArgs);
                    break;

                case "Add":
                    Console.WriteLine(AddPet(commandArgs));
                    break;

                case "Release":
                    Console.WriteLine(ReleasePet(commandArgs));
                    break;

                case "HasEmptyRooms":
                    string clinicName = commandArgs[0];
                    this.clinic = GetClinic(clinicName);
                    Console.WriteLine(clinic.HasEmptyRooms);
                    break;

                case "Print":
                    Console.WriteLine(Print(commandArgs));
                    break;

                default:
                    throw new InvalidOperationException("Invalid command!");
            }
        }

        private string Print(string[] commandArgs)
        {
            string clinicName = commandArgs[0];
            this.clinic = GetClinic(clinicName);

            if (commandArgs.Length == 1)
            {
                return this.clinic.Print();
            }
            else
            {
                int room = int.Parse(commandArgs[1]);

                return this.clinic.Print(room);
            }
        }

        private bool ReleasePet(string[] commandArgs)
        {
            string clinicName = commandArgs[0];
            this.clinic = GetClinic(clinicName);

            return this.clinic.Release();
        }

        private bool AddPet(string[] commandArgs)
        {
            string petName = commandArgs[0];
            string clinicName = commandArgs[1];

            this.pet = GetPet(petName);
            this.clinic = GetClinic(clinicName);

            return this.clinic.Add(this.pet);
        }

        private void Create(string[] commandArgs)
        {
            string typeToCreate = commandArgs[0];
            commandArgs = commandArgs.Skip(1).ToArray();

            if (typeToCreate == "Pet")
            {
                string name = commandArgs[0];
                int age = int.Parse(commandArgs[1]);
                string kind = commandArgs[2];

                this.pet = new Pet(name, age, kind);
                this.pets.Add(pet);
            }
            else
            {
                string name = commandArgs[0];
                int rooms = int.Parse(commandArgs[1]);

                this.clinic = new Clinic(name, rooms);
                this.clinics.Add(clinic);
            }
        }

        private Clinic GetClinic(string clinicName)
        {
            this.clinic = this.clinics.FirstOrDefault(x => x.Name == clinicName);

            if (clinic == null)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            return clinic;
        }

        private Pet GetPet(string petName)
        {
            this.pet = this.pets.FirstOrDefault(x => x.Name == petName);

            if (pet == null)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            return pet;
        }
    }
}
