﻿using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysSynchronizeSystem]
public class PaddleMovementSystem : JobComponentSystem
{
	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		float deltaTime = Time.DeltaTime;
		float yBound = GameManager.main.yBound;

		//Mainthread
		Entities.ForEach((ref Translation trans, in PaddleMovementData data) =>
		{
			trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);
		}).Run();

		return default;

		//if i would multithread/workerthreads without '[AlwaysSynchronizeSystem]'
		/*JobHandle myJob = Entities.ForEach((ref Translation trans, in PaddleMovementData data) =>
		{
			trans.Value.y = math.clamp(trans.Value.y + (data.speed * data.direction * deltaTime), -yBound, yBound);
		}).Schedule(inputDeps);

		return myJob;*/
	}
}