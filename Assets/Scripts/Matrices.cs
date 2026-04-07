using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Matrices
{
    public static Matrix4x4 CreateModelMatrix(Vector3 newPosition, Vector3 newRotation, Vector3 newScale)
    {
        Matrix4x4 positionMatrix = new Matrix4x4(
            new Vector4(1f, 0f, 0f, newPosition.x), // Primera columna
            new Vector4(0f, 1f, 0f, newPosition.y), // Segunda columna
            new Vector4(0f, 0f, 1f, newPosition.z), // Tercera columna
            new Vector4(0f, 0f, 0f, 1f)             // Cuarta columna
        );
        positionMatrix = positionMatrix.transpose;

        Matrix4x4 rotationMatrixX = new Matrix4x4(
            new Vector4(1f, 0f, 0f, 0f), // Primera columna
            new Vector4(0f, Mathf.Cos(newRotation.x), -Mathf.Sin(newRotation.x), 0f), // Segunda columna
            new Vector4(0f, Mathf.Sin(newRotation.x), Mathf.Cos(newRotation.x), 0f),  // Tercera columna
            new Vector4(0f, 0f, 0f, 1f) // Cuarta columna
        );

        Matrix4x4 rotationMatrixY = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.y), 0f, Mathf.Sin(newRotation.y), 0f), // Primera columna
            new Vector4(0f, 1f, 0f, 0f), // Segunda columna
            new Vector4(-Mathf.Sin(newRotation.y), 0f, Mathf.Cos(newRotation.y), 0f), // Tercera columna
            new Vector4(0f, 0f, 0f, 1f) // Cuarta columna
        );

        Matrix4x4 rotationMatrixZ = new Matrix4x4(
            new Vector4(Mathf.Cos(newRotation.z), -Mathf.Sin(newRotation.z), 0f, 0f), // Primera columna
            new Vector4(Mathf.Sin(newRotation.z), Mathf.Cos(newRotation.z), 0f, 0f),  // Segunda columna
            new Vector4(0f, 0f, 1f, 0f), // Tercera columna
            new Vector4(0f, 0f, 0f, 1f) // Cuarta columna
        );

        Matrix4x4 rotationMatrix = rotationMatrixZ * rotationMatrixY * rotationMatrixX;
        rotationMatrix = rotationMatrix.transpose;

        Matrix4x4 scaleMatrix = new Matrix4x4(
            new Vector4(newScale.x, 0f, 0f, 0f), // Primera columna
            new Vector4(0f, newScale.y, 0f, 0f), // Segunda columna
            new Vector4(0f, 0f, newScale.z, 0f), // Tercera columna
            new Vector4(0f, 0f, 0f, 1f) // Cuarta columna
        );
        scaleMatrix = scaleMatrix.transpose;

        Matrix4x4 finalMatrix = positionMatrix;
        finalMatrix *= rotationMatrix;
        finalMatrix *= scaleMatrix;

        return finalMatrix;
    }


    public static Matrix4x4 CreateViewMatrix(Vector3 cameraPos, Vector3 targetPos, Vector3 worldUp)
    {
        // Calcular el eje Forward (F), es la dirección desde la cámara hacia el objetivo, normalizada.
        Vector3 F = (targetPos - cameraPos).normalized;

        // Calcular el eje Right (R).Es el producto vectorial entre Forward y el "Arriba" del mundo.Usamos Cross(F, worldUp) para seguir la regla de la mano derecha.
        Vector3 R = Vector3.Cross(F, worldUp).normalized;

        // Calcular el eje Up (U) corregido. Producto vectorial entre Right y Forward para asegurar que todos sean ortogonales.
        Vector3 U = Vector3.Cross(R, F).normalized;

        // Construir la matriz de Vista 
        Matrix4x4 v = Matrix4x4.identity;

        // Fila 0 (Eje Right y traslación)
        v[0, 0] = R.x; v[0, 1] = R.y; v[0, 2] = R.z;
        v[0, 3] = -Vector3.Dot(R, cameraPos);

        // Fila 1 (Eje Up y traslación)
        v[1, 0] = U.x; v[1, 1] = U.y; v[1, 2] = U.z;
        v[1, 3] = -Vector3.Dot(U, cameraPos);

        // Fila 2 (Eje Forward invertido y traslación)
        v[2, 0] = -F.x; v[2, 1] = -F.y; v[2, 2] = -F.z;
        v[2, 3] = Vector3.Dot(F, cameraPos);

        // Fila 3 (Homogénea)
        v[3, 0] = 0f; v[3, 1] = 0f; v[3, 2] = 0f;
        v[3, 3] = 1f;

        return v;
    }
}


