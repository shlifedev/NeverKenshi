using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Drawing;
using Sirenix.OdinInspector;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;
using UnityEditor;
using UnityEngine.Jobs;
using UnityEngine.Profiling;
using Vector2 = System.Numerics.Vector2;


/// <summary>
/// 정사각형 그리드를 생성혹은 기존 위치를 변경합니다.
/// </summary>
[BurstCompile]
public struct UpdateGridJob : IJobParallelFor
{
    /// <summary>
    /// 그리드 크기 (32인경우 32*32)
    /// </summary>
    public int size;
    /// <summary>
    /// 그리드 오프셋용 트렌스폼
    /// </summary>
    public float3 offset;
    /// <summary>
    /// 그리드 위치 
    /// </summary>
    public NativeArray<float3> gridPoints; 
    public void Execute(int index)
    {
        
        int x = index % size; // n이 256인경우 
        int z = index / size; 
        float3 point = new float3(offset.x + x, offset.y + 0, offset.z + z);
        gridPoints[index] = point;   
    }
} 
 
/// <summary>
/// 그리드 컬라이젼을 위해 잡 세팅
/// </summary>
[BurstCompile]
public struct SetUpGridCollisionJob : IJobParallelFor
{ 
    /// <summary>
    /// 그리드 정보
    /// </summary>
    [Unity.Collections.ReadOnly]  public NativeArray<float3> gridPoints;
    /// <summary>
    /// 레이어 마스크 등
    /// </summary>
    [Unity.Collections.ReadOnly]  public QueryParameters queryParameters;  
    [WriteOnly] public NativeArray<RaycastCommand> allocCmds;
    public void Execute(int index)
    {  
        allocCmds[index] = new RaycastCommand()
        {
            from = gridPoints[index] + new float3(0,1,0) * 100,
            direction = Vector3.down,
            distance = 9999,
            queryParameters = queryParameters
        };   
    }
}




public class GridManager : Drawing.MonoBehaviourGizmos
{  
    public event System.Action OnInitialized;  
    [SerializeField] int gridSize;  
    /// <summary>
    /// Grid Managed Points
    /// </summary>
    private NativeArray<float3> managedGridPoints;
    
    /// <summary>
    /// 기즈모 중심위치  
    /// </summary>

    [SerializeField] Vector3 offset;

 
 
    [Button("Test")]
    public void TryHitCollsionGrid()
    {
        var raycastHits = new NativeArray<RaycastHit>(gridSize * gridSize, Allocator.TempJob);
        var raycastCommands = new NativeArray<RaycastCommand>(gridSize * gridSize, Allocator.TempJob);

        var job = new SetUpGridCollisionJob()
        {
            gridPoints = this.managedGridPoints, 
            queryParameters = new QueryParameters()
            { 
            },
            allocCmds = raycastCommands
        }; 
        
        
        // 여기서는 그리드 사이즈만큼 commands를 만들어 세팅한다.
        var setUpHandle = job.Schedule(gridSize * gridSize, 32);
        setUpHandle.Complete(); 
        // 여기서는 레이케스트 힛을 날린다.
        var commandHandle = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits, 32, default(JobHandle));
        commandHandle.Complete();

        
        // 메모리에서 해제한다.
        raycastCommands.Dispose();
        raycastHits.Dispose(); 
    }
 
    
    public void UpdateGrid(int gridSize, float3 worldOffset)
    {
        this.offset = worldOffset;
        this.gridSize = gridSize;
        int totalPoints = gridSize * gridSize;
        
        // created 되지 않거나 혹은 size가 다른경우 생성
        if (!managedGridPoints.IsCreated || totalPoints != managedGridPoints.Length) 
            managedGridPoints = new NativeArray<float3>(totalPoints, Allocator.Persistent); 

        if (this.managedGridPoints.IsCreated)
        {
            this.UpdateGridPosition();
        }
        else
        { 
            this.CreateGridAsync();
            this.UpdateGridPosition();
        } 
    }
 

    public void Update()
    {
        this.UpdateGrid(this.gridSize, this.transform.position); 
    }
 

    void UpdateGridPosition()
    { 
        int totalPoints = gridSize * gridSize; 
        UpdateGridJob updateGridJob = new UpdateGridJob
        {
            size = gridSize,
            gridPoints = managedGridPoints,
            offset = this.offset,  
        };
  
        var handle = updateGridJob.Schedule(totalPoints, 32);
        handle.Complete(); 
        
    }
    /// <summary>
    /// size * size로 그리드 포인트가 생성됩니다.
    /// </summary> 
    void CreateGridAsync()
    {  
        
        int totalPoints = this.gridSize * this.gridSize;  
        // n * n 포인트 생성 
        // 포인트 수에따라 적절히 작업배분 하게 함. 이런 용도 맞나.
        int batchCount = 64;
     
        UpdateGridJob updateGridJob = new UpdateGridJob
        {
            offset = this.offset,
            size = gridSize,
            gridPoints = managedGridPoints
        };  
         
        var jobHandle = updateGridJob.Schedule(totalPoints, batchCount);
        jobHandle.Complete();
        OnInitialized?.Invoke();  
    }
 
    public void OnDestroy()
    {
        managedGridPoints.Dispose();
    } 
}