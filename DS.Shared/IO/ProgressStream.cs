namespace DS.Shared.IO;

/// <summary>
///     Поток, с вызовом <see cref="IProgress{T}"/>.
/// </summary>
public class ProgressStream : Stream
{
    private Stream _input;
    private long _length;
    private long _position;
    private int oldProgress = -1;
    private IProgress<int> _progressCallback;

    /// <inheritdoc/>
    public override bool CanRead => true;

    /// <inheritdoc/>
    public override bool CanSeek => false;

    /// <inheritdoc/>
    public override bool CanWrite => false;

    /// <inheritdoc/>
    public override long Length => _length;

    /// <inheritdoc/>
    public override long Position { get => _position; set => throw new NotImplementedException(); }

    /// <summary>
    /// 	Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="stream">Поток.</param>
    /// <param name="progressCallback">Функция обратного вызова для отображения процесса отправки.</param>
    public ProgressStream(Stream stream, IProgress<int> progressCallback)
    {
        ThrowHelper.ArgumentNotNull(stream);
        ThrowHelper.ArgumentNotNull(progressCallback);

        _input = stream;
        _length = stream.Length;
        _progressCallback = progressCallback;
    }

    /// <inheritdoc/>
    public override void Flush()
    {
        _input.Flush();
    }

    /// <inheritdoc/>
    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _input.Read(buffer, offset, count);
        _position += bytesRead;
        int progress = (int)(_position * 100 / _length);
        if (progress != oldProgress)
        {
            oldProgress = progress;
            _progressCallback!.Report(progress);
        }
        return bytesRead;
    }

    /// <inheritdoc/>
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _input.Dispose();
        }
        base.Dispose(disposing);
    }
}
