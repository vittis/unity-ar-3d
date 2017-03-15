using UnityEngine;
using System.Collections;
using System.Linq;

public class PlaneMesh : MonoBehaviour {
    public int linhas, colunas;

    public Vector3 ponto1, ponto2, ponto3, ponto4;

    Vector3[] vertices;
    Vector3[] pontos, pontos2;
    MeshFilter mf;
    Mesh mesh;

    void Start() {
        Vector3[] pontos = new[] {
            ponto1,
            ponto2,
            ponto3,
            ponto4
            };

        BuildMesh(pontos);
    }

    public void BuildMesh(Vector3[] pontosq) {
        for (int i = 0; i < 4; i++) {
            pontosq[i].y = 0;
        }

        ponto1 = pontosq[0];
        ponto2 = pontosq[1];
        ponto3 = pontosq[2];
        ponto4 = pontosq[3];

        mf = GetComponent<MeshFilter>();
        mesh = new Mesh();

        int[] tri;
        Vector2[] uv = new Vector2[(linhas + 1) * (colunas + 1)];
        Vector3[] normals = new Vector3[(linhas + 1) * (colunas + 1)];
        //vertices
        vertices = new Vector3[(linhas + 1) * (colunas + 1)];
        pontos = new Vector3[linhas + 1];
        for (int i = 0; i < linhas + 1; i++) {
            pontos[i] = Vector3.Lerp(ponto1, ponto3, (float) 1 / linhas * i);
        }
        pontos2 = new Vector3[linhas + 1];
        for (int i = 0; i < linhas + 1; i++) {
            pontos2[i] = Vector3.Lerp(ponto2, ponto4, (float) 1 / linhas * i);
        }
        for (int k = 0; k < linhas + 1; k++) {
            Vector3 pont1, pont2;
            pont1 = pontos[k];
            pont2 = pontos2[k];
            for (int i = 0; i < colunas + 1; i++) {
                vertices[i + ((colunas + 1) * k)] = Vector3.Lerp(pont1, pont2, (float) 1 / colunas * i);
                //linha que mexe na altura
                vertices[i + ((colunas + 1) * k)].y = Random.Range(-0.5f, 1.5f);
                //
                normals[i + ((colunas + 1) * k)] = Vector3.up;
                uv[i + ((colunas + 1) * k)] = new Vector2((float) i / colunas + 1, (float) k / colunas + 1);
            }
        }
        //tri
        tri = new int[linhas * colunas * 2 * 3];
        int indice = 0;
        for (int i = 0; i < linhas; i++) {
            for (int j = 0; j < colunas; j++) {
                int q = ((colunas + 1) * i) + j;
                tri[indice] = q;
                tri[indice + 1] = q + 1;
                tri[indice + 2] = q + colunas + 1;
                tri[indice + 3] = q + 1;
                tri[indice + 4] = q + colunas + 2;
                tri[indice + 5] = q + colunas + 1;
                indice += 6;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = tri;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        mesh.normals = normals;
        mesh.uv = uv;
        mf.mesh = mesh;
    }






    /*IEnumerator EfeitoOnda() {
        for (int i=0; i< vertices.Length; i++) {
            vertices[i].y = Random.Range(-32.5f, 22.5f);
            vertices[vertices.Length-1-i].y = Random.Range(-32.5f, 22.5f);
            print("sd");
            yield return new WaitForSeconds(0.05f);
            mesh.vertices = vertices;
        }
        
        mesh.RecalculateBounds();

        mf.mesh = mesh;
    }*/

    /*void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(ponto1, 0.1f);
        Gizmos.DrawSphere(ponto2, 0.1f);
        Gizmos.DrawSphere(ponto3, 0.1f);
        Gizmos.DrawSphere(ponto4, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ponto1, ponto2);

        Gizmos.color = Color.blue;
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }

    }*/
}