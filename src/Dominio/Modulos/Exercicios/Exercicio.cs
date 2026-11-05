using LiftingView.Dominio.Compartilhado;

namespace LiftingView.Dominio.Modulos.Exercicios;

public sealed class Exercicio : EntidadeBase<Exercicio>
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string GrupoMuscular { get; set; } = string.Empty;

    public Exercicio()
    {
    }
    public Exercicio(string nome, string descricao, string grupoMuscular) : this()
    {
        Nome = nome;
        Descricao = descricao;
        GrupoMuscular = grupoMuscular;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome do exercício é obrigatório.");

        if (string.IsNullOrWhiteSpace(Descricao))
            erros.Add("A descrição do exercício é obrigatória.");

        if (string.IsNullOrWhiteSpace(GrupoMuscular))
            erros.Add("O grupo muscular do exercício é obrigatório.");

        return erros;
    }

    public override void Atualizar(Exercicio entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;

        Descricao = entidadeAtualizada.Descricao;

        GrupoMuscular = entidadeAtualizada.GrupoMuscular;
    }
}
