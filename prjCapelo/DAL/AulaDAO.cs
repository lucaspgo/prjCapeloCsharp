﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using prjCapelo.Models;
using Microsoft.EntityFrameworkCore;

namespace prjCapelo.DAL
{
    class AulaDAO
    {
        private static Context _context = SingletonContext.GetInstance();
        
        public static bool Cadastrar(Aula aula)
        {
            try
            {
                _context.Aula.Add(aula);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public static Boolean Remover(Aula aula)
        {
            try
            {
                _context.Aula.Remove(aula);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        public static void Alterar(Aula aula)
        {
            _context.Aula.Update(aula);
            _context.SaveChanges();
        }
        public static List<Aula> Listar() =>
            _context.Aula.ToList();
        public static Aula BuscarPorId(int id) =>
            _context.Aula.Find(id);

        public static List<Aula> BuscarPorProfessoreData(int idProfessor, DateTime data) =>
            _context.Aula.Include(x => x.Professor.Pessoa).Where(x => x.Professor.Matricula == idProfessor).Where(x => x.Data == data).ToList();

        public static List<Aula> BuscarPorMatricula(int matricula) =>
            _context.Aula.Include(x => x.Professor).ThenInclude(prof => prof.Pessoa).Include(x => x.Professor).ThenInclude(prof => prof.Disciplina).Include(x => x.Aluno.Pessoa).Include(x => x.Professor).Include(x => x.Sala).Include(x => x.Aluno.Pessoa).Where(x => x.Aluno.Matricula == matricula).ToList();
    }
}
