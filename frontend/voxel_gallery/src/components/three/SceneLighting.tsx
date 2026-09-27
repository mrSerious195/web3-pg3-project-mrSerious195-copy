const SceneLighting = () => {
  return (
    <>
           <ambientLight intensity={0.65}></ambientLight>
        <directionalLight position={[-1, 0, -3]} intensity={0.25} />
        <directionalLight position={[1, 5, 3]} intensity={0.75} />
        </>
  )
}

export default SceneLighting