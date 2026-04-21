namespace Apprendre;

public partial class Apprendre
{
    #region Properties

    private const string ImageLabelText = "Image";
    private const string ImageIconText = "📷";

    private readonly Dictionary<Label, (Color CouleurTexte, Color CouleurFond)> _apparencesInitialesDesLabelsImage = [];

    #endregion Properties

    #region Private

    private void InitializeHideImageOnNonImageClicks()
    {
        RegisterHideImageOnNonImageClicks(this);
    }

    private void RegisterHideImageOnNonImageClicks(Control control)
    {
        control.ControlAdded -= ControlHideImage_ControlAdded;
        control.ControlAdded += ControlHideImage_ControlAdded;

        if (!ReferenceEquals(control, WebView21))
        {
            control.Click -= ControlHideImage_Click;
            control.Click += ControlHideImage_Click;
        }

        foreach (Control childControl in control.Controls)
        {
            RegisterHideImageOnNonImageClicks(childControl);
        }
    }

    private void ControlHideImage_ControlAdded(object? sender, ControlEventArgs e)
    {
        RegisterHideImageOnNonImageClicks(e.Control);
    }

    private void ControlHideImage_Click(object? sender, EventArgs e)
    {
        if (sender is Label label && EstLabelImage(label))
        {
            return;
        }

        HideSelectedImage();
    }

    private void HideSelectedImage()
    {
        if (WebView21.IsDisposed || !WebView21.Visible)
        {
            return;
        }

        ExecuteWithoutResettingScroll(() => WebView21.Visible = false);
        _currentItemIndex = -1;
        _currentChildItemIndex = -1;
    }

    private async void button1_Click(object? sender, EventArgs e)
    {

        await WebView21.EnsureCoreWebView2Async();

        string dataUrl1 = "data:image/jpeg;base64,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";
        string html1 = $"""
                <!doctype html>
                <html>
                <body style="margin:0;overflow:hidden;">
                    <img src="{dataUrl1}" style="display:block;width:100%;height:100%;object-fit:contain;object-position:45% center;" />
                </body>
                </html>
                """;

        WebView21.NavigateToString(html1);

    }

    private async void LabelImage_Click(object? sender, EventArgs e)
    {
        if (sender is not Label label)
        {
            return;
        }

        await ShowSelectedImageAsync(label);
    }

    private void LabelImage_Disposed(object? sender, EventArgs e)
    {
        if (sender is not Label label)
        {
            return;
        }

        label.Disposed -= LabelImage_Disposed;
        _apparencesInitialesDesLabelsImage.Remove(label);
    }

    private void LabelImage_MouseEnter(object? sender, EventArgs e)
    {
        if (sender is not Label label)
        {
            return;
        }

        _apparencesInitialesDesLabelsImage.TryAdd(label, (label.ForeColor, label.BackColor));
        label.ForeColor = Color.RoyalBlue;
        label.BackColor = Color.FromArgb(230, 240, 255);
    }

    private void LabelImage_MouseLeave(object? sender, EventArgs e)
    {
        if (sender is not Label label || !_apparencesInitialesDesLabelsImage.TryGetValue(label, out (Color CouleurTexte, Color CouleurFond) apparenceInitiale))
        {
            return;
        }

        label.ForeColor = apparenceInitiale.CouleurTexte;
        label.BackColor = apparenceInitiale.CouleurFond;
    }

    private async Task ShowSelectedImageAsync(Label label)
    {
        if (!EstLabelImage(label))
        {
            return;
        }

        await ShowSelectedImageForLabelAsync(label);
    }

    private async Task ShowSelectedImageForLabelAsync(Label label)
    {
        string? tag = label.Tag?.ToString();

        if (string.IsNullOrEmpty(tag))
        {
            _currentItemIndex = -1;
            _currentChildItemIndex = -1;
            return;
        }

        string[] tagParts = tag.Split('|');


        _currentItemIndex = tagParts.Length > 1 && int.TryParse(tagParts[1], out int itemIndex)
            ? itemIndex
            : -1;

        _currentChildItemIndex = tagParts.Length > 2 && int.TryParse(tagParts[2], out int childItemIndex)
            ? childItemIndex
            : -1;

        if (_currentItemIndex < 0)
        {
            return;
        }

        string imageUrl = string.Empty;

        if (_dataList.Count > 0)
        {
            imageUrl = _dataList[_currentItemIndex].Url;
        }
        else if (_animauxMFPList.Count > 0)
        {
            imageUrl = _animauxMFPList[_currentItemIndex].Url;
        }

        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return;
        }

        if (!Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri? imageUri))
        {
            return;
        }

        if (label.Parent != null)
        {
            Point scrollPosition = CaptureScrollPosition();
            Rectangle imageBounds = GetImageBounds(label.Parent);

            ExecuteWithoutResettingScroll(() =>
            {
                WebView21.Visible = false;
                WebView21.Bounds = imageBounds;
                WebView21.BringToFront();
            });

            await WebView21.EnsureCoreWebView2Async();

            //string dataUrl1 = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxESEhUTExIVFRUWFxcaFRYVFRcVFRUXFhcYFxcVFRUYHSggGRolHRUVITEhJSkrLi4uGh8zODMtNygtLisBCgoKDg0OGhAQGy0lHyUtLS0vLy8tLS0rLSstLS0vLS0tLS0tLS0tLS0tLS0tLS0tLTAtLS0tLy0tLS0tLS0tLf/AABEIANMA7wMBIgACEQEDEQH/xAAcAAABBQEBAQAAAAAAAAAAAAAAAgMEBQYHAQj/xABBEAABAwIDBQUHAwEGBQUAAAABAAIRAyEEMUEFElFhcQYigZGhBxMyQrHB8FLR4SMUYoKSovEVM3LC4hckQ4OT/8QAGgEAAgMBAQAAAAAAAAAAAAAAAAMBAgQFBv/EADERAAICAQMBBgQFBQEAAAAAAAABAgMRBCExEgUTIjJBUWFxsfCBkaHR4RRCUnLBI//aAAwDAQACEQMRAD8A7ihCEACEIQAIQhAAhCEACEIQAIQhAAhCEACEIQAIQhAAhCbrV2sEucGiYlxAE8LoAcQqpnaTBEEjE0iBEnfEd7K6kYTa2HqgGnWpvBJA3XtNwJIzzAVepe5HUiaheNINxdeqxIIQhAAhCEACEIQAIQhAAhCEACEIQAIQhAAhCEACEIQAIXhKrNo7doURLnTyChyS3ZaMJTeIrJkNudratJ53qnug17pB3CY0aW8Yk6rQdie0P9rwNLE1CGlxqAkw2dyo5gMdGhcu9pW0KGNh9OkGvZMvAgvBHzcYgX6rPdn8bVNFtNhMAkAA8TJ9SVjjaoZbeTqrQOaSl4fU+gMT2iwtP4qo8FUv7eYWd1m888svNc/wbaVIAv79TiZgdBMeJTePxrawiQ0jJ0f6SG3IP2VHrlkwOemhLG7Xv/Bt+03bY08M51EbtVxDWEkO3S75oiCQAbFcgxletUeX1nve5xJlxJvx5C+ngtRXDKjKbC4Q1287dBgkAgRMaEkyitsmjUtvOnMOMQDzETH5ySrb4WPzGDVKMp/+fBQYcuaIORy4J6hh3k5WOpsP5Wg/4OKYAs4Gd05jmADwMZhODBGfFXVSaMjWBjA7WxeDE0ame6Cx3ebDTMAGwm4toT1Wv7O+0ijUAZih7qoXQC1rjTIMbs5kEknlbNZPGYUuZAF7fVZrF4J4+U2PCDnz8LqXKdfl4LxslHg+jULhHZPtZWwLjnUpkR7o1IYCXTvCx3Tn1m67JsPb2HxbXGg/e3CA4QQWk5WIyMG61VXxs24fsaq7VP5lmhCE8aCEIQAIQhAAhCEACEIQAIQhAAhCqO0XaCjg2bzzLj8DBm7nyHNQ2kssvCuVklGKy2Wdas1gLnENAzJMBUWK7UU7+7vHzEGPALAbQ7VVq7t52Xyt+UdAoj9o1nfzb6rJbdJrEDsU9ldO9pp9o9pHOsHb3T9lksfXNQySZ4H7r2o2s75WnmHBNVMNVBncd6nyXJsjfnLbZ0oRrpXhHcFhWuMEZ6H1VPsnD/2d9alwIjqCZ+yuqdGpY7pbGptkq/a9c08S2pTgkxvDgY3XR1sqVQt3T9RV8XqYuEfVff5jVapJi88IM+Sks2dWLd4Nif1ED0TjseWS8MDZMucRJk6klDsWXTeTqmxoil4mY6ew8rxv8itditwwb9P5T+H2iw5P8DY+sT4JvFG+Wd7+irMURfuj0uFDorfGxqn2DVJeFtfqa5m0YAsSNQbeIPHqrrCVmVfgInOIO8PA5+C5WzFPp3Y4gfpI7vlp4K1wG3WEjePu3jI/LPJ2n5dMjK2njdHE13Yt1XiSyvh+x0SrhzH1Gl81R7T2ec4U7Zm32wG1s9Hi4PWPqFd+6a8SCHA5EGQfFbYX12rY4Tg0cxxuELTknNibcxGEqb9F5B1aTLHcd5lgcs8xyW12nstpBlo8li9oYAMNhbqZHmk21uPiiUy1wd72RtGniKTatNzXAi+6ZAd8zfAqYuM+zvtA7DVhSduCjVd3y7u7piGunLgIXZlvouVkc+p0KrOuOQQhCeNBCEIAEIQgAQhCABCEmo8NBJMACSTkAMyUAVXaXbtPB0t913G1NmrnfYDMn+FybF1n4iq6rVdvOJudLZNbwaOCmbX2icdiveEHcEim0ZhgNjHE5ny0SqGF3sspNuRP+y5Gp1WZYXCPX6DSR0teZeZ8/D4FXWqbgsJOnJQ99xPeyWhrbOAFxc/l1BxOEJECb8uGv5xS4355N0VCXBFbtE/CxsRmf5VjhsYY705WHNV7cJumOGdvG/E3Twpv3i0ggAQba/dN74rLTwYqtie9nB6zeOX5xUOhTBMm5/PP+U6zCVN7eI1jXXgFoNnbIbAJB6H7hVn1SWxDlXSjP1cO54MAxrwiFBw1FzG3MgWMzMaeS3FbZu6e6S3esb6agjgSFRY5oDiIniOPFK6GtmWq1Klsijq8ALeAUGqLxnz/AJVhjD5DT6KG46H68VZRNaexBqYTuz16eXCSorqQGYy9VbUnAgjdi/gP4TFek0sP6hysRx5aJkRU36MiYPFvpfDdurDkenBavYG3SL0zaxewxP8AHULH1dI4JG8Qd5pgjUWIKLNOp+JbP3ODr+za7vFHaX3ydpwmOp1myw9Qc29ePUKn2zgGu/ukzfTLUfmayvZvtAd6J3agHhUHCPt5LZOrCs0louM2yJHPmFNVzb7u3zfX5Hj9Tp5VSxJGNxmGLHHI9D49YXYOwG2BiMK0GoX1afdqb3xXJLCeI3bTrBXLNoMvex4cORGivvZbjjTxbqUtDarCYIuXMu0NI5F9ss0USVd2Fw9hNE+mePc60hCF1jpAhCEACEIQAIQhAAsp7R9o+6whpj4qx3P8ES/07v8AiWrXLfapjJxNOloynveL3GfRjUnUS6a2dHsqnvdVFPhb/l/JlsPVgWta/PNT8Fjtw31z+iozJNiY4c0+7kTbRcdwTPbTgpLDNgMSx48Pz7r11FpjIZg3zn/ZZnA1iPtyuLzxVnTxfOSL+QzCV3TTMM6HF+FlmzZsOk/nh5KdT2cwG0nKZ1J6prD45rqbRPe3hPKNI8lYMfE3zjxWiqCOfbZZ6jLcG0Xic0t1oScRWAFz6/ZV9bH2tpny6rWmuBSjKW5ZVKrd26xu2K7Tdrpv4/mSfxu0Lw0nWfPQKsbUEODjGZEAE70RE8Eme7N+mp6PEysr1TfmTpwE/uorwTYmwEg9bwk4kzPjBy8+Oq8ZUuOkGRzzUI6uNtiXh6oY/dqMneZYTGYsZUFzs87fReVnmb2jLT81TbnGIDrWOWsZHzVkhMo43GXi35dR31CTfX7KQ4x+XTb22jyTomO7cjVma6j8BWp7M7fLiGvI9434Scnj9+IWaJKh1ZBEGCDYixBGRVbaVbHD59H7HF1lEbVhnXca5lanvFvW92nqLwVWbFr/ANmxdGp8oeJO6HmDYiDrBNx4cFUdmNu+8G66N8CHiPib+oDQ/dXtTBua5rmkuFi1wFxexjisalJvEl4o/eTy11DrkdrQksMgHl09Epd02ghCEACEJl2KaDE+SrKSjyyUm+B5CS1wORSlYgFxf2iVJ2hW5CmP9DT912hcU9odMjaNbmKZH/5tH2WfUrwfidnsN41D/wBX9UULqgHon6XwzNyftI+6gfYKRQfoVz2j13oWGGdumDlH1UhlIQRzBnzUalEzp6qWyrDYjMjj+aqMC5v2HcMe9cwrOvtQsaJIzHPMaX5qhfVDSDGX5+6Tjq3dm4y0gZW+ilIROpTayaPFYlrtQciY/LKj2hig5zw0QDfpyUSni4HWMp6ymKjgJBOdunND2CujpY1UfHj4KNUrF2RjMX58gmyd4xOt9T1A8EnHU3UahY4gxrNvRSjVhJ49SPVMEjQC3UJDHzbXj9Z/dKq35Wv16pgui8cVI1PYkVGTlfoCcuKaOZnVeMqEwNJSH1MzrlyhXRnm/QU/jom5Igzl9l4XfRJi3kmHOnIU87xJUKu26lM1TdUDVWTMdiyXXsuwjKu06NN4lr21gR/9TyCDoQQCCunO2S/D1DQfcGXUn6GOHB3HmOawXsdob21aZ/RTqu/07n/eu97TwTarIIuLsOrXaGfQ8lNlHeJTXK+nscHVxXXh+xJpA7onOBPXVKSaeQ6BKWwzghCjY2pAA4/TVVlLpWWSll4Gq9cusMuPH+E0GQlNanAFzZtzeWP2XA0JT9Oq7r1Rur1TX1R4ZEmmOtrDWy5V7WMOG4unUB/5lKOppuM+j2rp7ioG0sJTqsh9JtRvBwBA88kyy5uOGP0V3cWqfocHpVAZm2eXHSUrDVAHj1XQtqdhcK6XMLqGp728zxD8h0IWZxnY7EUz3HU6w03HtD4/6CfoSk4yenp7S09n92Pnt/BCpOE58Pz1UpzYIB+aI+qrTTfTdD2OaeDgWnyKedjTvME2aLcpMn6lRg2Z6uCTi33M5qNiILZzOn1hKxVYOdoVFqPkFSi8eEIbXJgA5W5gmUmtVygjK/4VCrP3Yg3T28IF7R5omi7whTzfO+ljrx8kxXYXPBJE/Ya/RPOdbOcp8Ao7pJn0HBURTIYl5Npi2X26pFSsTTawtAIJMx3rxny/de1ZJvrpl+CSEwQc8/2UoMrAhjfz7JVV0gmI4he4hgB7pkCP5skNnd8U1GeyeRqsfhKc3m2iZi/I3y5Jt7e7+eKcpi0pnoYJ8noIEj8hN1BZPOTAP39EITI6H7CsFOLxFXSnRDPGq8H6Ul2xc99iezPd4J1Yi9eq4j/op9xvqHnxXQlrgvCcDVSzawQhCuZwUHEmX9AFOUCue+fD6BI1HlL18iw1ekpLSgrKkXYqUSkohDZAziqwAuvaT4TWKo7wITVCrFjn9Vmc2rPFwNwnHYj9o8EyrQqMd8L2kHiDmHDmDB8FxfAbVaN5tWPeixBAs5pg7s6SJXdnPB4FU1bs3gHPNR2Ew7nm+8aTCZ4mQnuxDaLO7zlZMt2LxrsXTqMqNNRjPhe8bzXB2TJPxER5EKzr9iMNUFt6m7iw2/ymR5QtRTogABoAAyAEAdAE4GpPT1T6kXjqpw8jwc42h7PsS29GoypyP9N0cswT4hZraOzcTQJ97ReyI7zmd0/4xLZ8V24BLaU5I119r3R8yT/R/f4HzliXiPyITtMO3QdNDx0MFdw2n2RwGIk1MOwOPz0/6bp4ksifGVlNp+y4R/7fFOAvDKzd4X4PbEeRUtbHQr7Ypn5sr7+BzkcTwsNJP56p2jUjekX+W+RsZ56iFM2/2YxeEE1WS2fjpu32DrkR4gKqZ9NPVUwjarYzWYvJ664LvyxmE25sTwKdfUPhw9bpDhEg5acVOA6xkxZeEII8LeqBn+XV0hUpZG3G0Ta/qvW/CiLHp9UsMBA9fVWMs3ueaHijD4R9R7KVMS+o4NaP7zjuj1K9cLroPse2F73EuxTm9ygIZwNV4j/S0k/4mq8Vl4Mt1ihByOubIwDcPQpUGfDTY1g57oiepzUxCFsPPt53BCEIIBV2LtU6gH7fsrFV22u61r9GmHcg6BPnu+EpVyzAvX5j0FOBRaL5UlpWOLLyQoNXsIalFMKjZama2Ga7MJ/eXjkuUYy2aJTaIRwYCUzDgZKQQklLWngnwXdjEbq8IS0Qm4SKiYXoalwvFRslAAk1EouUfE1QBdUk9i8Y5ZnO0mIE02nVxtyDHz9R5rl2P2cC5xad0B3eA4TEgceS0HaDtA2piCW/AwFrD+oky9w5WAHTmqLB4nedUB1BPqq1R6Y78s9LptPKMM/AoS6V605KVtHDwA9vV8dYlRKfPonjW2nueuBI/PNJgpT7Sc0pov4IQuTENZ9EptksG8f7pRarozyY5gcG+tVZTpt3nvdutHM8eA1J0AX0T2a2MzB4enQZfdHedq95u5x6nyEDRYz2XdmPcM/tVYf1ag/pg502HXk53oI4ldBa9aa4YWTiay/rl0rhDyEgFKCaYj1CEIAEirTDmlrhIcCCOINiEtCAMXgMcaNZ2ErHvNvSef8A5qZ+Ez+sCx5grQ0XqD2y7Pf2ulLLVqd6bsp4sJ4GB0ICw2x+2FWiTTxDXO3SQTEVGkWIcDmudbF1y+B0IV9/HMefVHTg5KsqfZu1qVYTTeDym46jMKwbVQppmeVbi8MdleEpvfRvKcorgUSkkoRCMhgU0L1JDkl1RVckWSYslNucqzaG28PS/wCZVa0/pmXf5RdZfafbjShTJOhcPUNH3SW88GunR22cL9jXY3HNptLnOAA1NlzrtN2rdW3qdKQzV2RdyHAKm2hjq1YzVeTwGn5mo7Gx+eqhRO/pezY1eKW7/QhVqBcRyRh8OWlxg5R5q0FOBJzTT3WhMOhnbB5h6IA3SQd5oBGed46iAqDEUN1xbwJCvGAtGRnTz0VZtJ494eg84uiEt8GbVQwlIjhoIjVKdAEcEUwIlOMpyYATUYZSSQ2zNbLsZ2eDnNr1x3BdjD850c4fpHDXpnW7K2U2Q54mMm6ePHotlhapTYYyc7UTbWEa6ljFNo11msM8q2writKZypRwXdN6eaVDoFS2KwtjiF4F6ggEIQgAWO7cdjhiga1GG1wLjJtUDQ8HcD4HiNihVlFSWGMrslXLqifPJqvpOLSHU3tMHNrmngRoVcYXtbimR/V3gNHje8zn6rpnavsjQxzZPcqgd2q0Xtk14+dvLTQhcf29sHE4J+7WZ3Z7tRt6bujoseRgrn2UOHyO9p9TVqFiSWfZ/wDDT0u3tb5qbD0Jb+6kj2iNBh1A84f9O6sFRxcGRE8wCPEFJdUmZ9P4Slk0/wBFQ35ToX/qJww9uJqf+KS7t/UOVFg4d5zp8gFz8OKX7yOKMsbHs/T/AOP1Nfie2uMdIG4w8mHeH+YlVuK2nWqke+rVS35mg7vhAhvoqRlc6SPzinfelT8zTDSVx8sUhfuxJI7rbwMz4mLlSMNVIkNJE2MajgojqqVhsWWGWmDoRmDxlBq6NiRVpQV7hWt3gXzHLPwUR2IJJMkzmSlNrxc56BHAPOME3G1m7sARBJJ1/ut8I9VBz6m5+yYdiSfr1SqYcTAuSqyk3wQkoLcXWflyVWMHVqOJDDnabdM1qsFsdxiR+cStDgth8k6qpnG1uvjJ4XCMNhNguPxeQV1hdjxkFt6GxRwU2lskcFpVRyZawyWE2YeCucNgFfU9nAaKUzBhMjWkZZ3uRU0MIrLD0FLZh0+ykmYEuWRNFiktC8a1LAUlD0IQhAAhCEACEIQAKNjKLXtLHtDmkQWuAc0jgQc1JSHhAI5n2j9n9F0uw7jSP6DJp+Bzb6jksDtDZOIw5IewwPmbdp8R94XfMTQlUO0tl702WedEXwdPT9o2Q2lujioxFogW1i6996tztPsmCSd3yt9Fn8T2WeMpWZ1SR2au0aXy8FQcRZee/UqpsKsOfUFM/wDB6uo8v9lRxZrjq6nwxsYgGxMDW37JPvU+Nkv4J2nsl/BR0sv/AFlS9SMK4HGdOE8+S8YXHmrWlsd2oU2jsZ3BSq2Is7RivKVeFwBcf0jjr4LVbJ2dTYLC/E5leYPZRGivMJgiE+uvByNVrJWbNkrA4YcFfYTDBQcHhyrvDU1rSOROWRbKASxSCeaEqFYXkZFNKDE5CIQQIDUoBKQgDyF6hCABCEIAEIQgAQhCABCEIAQ5qafQBUheQgCvqYIHRRKuymnRXcLzdQBm6mxGnRR37AbwWqLF4aahpMspNcGSPZ9vBejYLeC1fugj3QUdKLd7L3Mu3Yo4J1mxxwWj90Ee7R0ojvGUdPZg4KTTwIGitfdr0MU4I6mRKWGhSWMSw1KUkHgC9QhBAIQhAAhCEACEIQAIQhAAhCEACEIQAIQhAAhCEACEIQAIQhAAhCEACEIQAIQhAAhCEACEIQAIQhAAhCEACEIQAIQhAH//2Q==";
            string html1 = $"""
                <!doctype html>
                <html>
                <body style="margin:0;overflow:hidden;">
                    <img src="{imageUrl}" style="display:block;width:100%;height:100%;object-fit:contain;object-position:45% center;" />
                </body>
                </html>
                """;

            WebView21.NavigateToString(html1);

            ExecuteWithoutResettingScroll(() =>
            {
                WebView21.Visible = true;
                WebView21.BringToFront();
            });
            RestoreScrollPosition(scrollPosition);
            EnsureControlVisibleVertically(WebView21);
        }
    }

    private Point CaptureScrollPosition()
    {
        return new Point(
            HorizontalScroll.Visible ? HorizontalScroll.Value : 0,
            VerticalScroll.Visible ? VerticalScroll.Value : 0);
    }

    private void ExecuteWithoutResettingScroll(Action action)
    {
        Point scrollPosition = CaptureScrollPosition();

        SuspendLayout();

        try
        {
            action();
        }
        finally
        {
            ResumeLayout(false);
            RestoreScrollPosition(scrollPosition);
        }
    }

    private void RestoreScrollPosition(Point scrollPosition)
    {
        if (IsDisposed || !IsHandleCreated)
        {
            return;
        }

        AutoScrollPosition = scrollPosition;

        BeginInvoke(() =>
        {
            if (IsDisposed || !IsHandleCreated)
            {
                return;
            }

            AutoScrollPosition = scrollPosition;
        });
    }

    private void EnsureControlVisibleVertically(Control control)
    {
        if (IsDisposed || !IsHandleCreated || !control.Visible)
        {
            return;
        }

        BeginInvoke(() =>
        {
            if (IsDisposed || !IsHandleCreated || !control.Visible)
            {
                return;
            }

            int currentScrollX = HorizontalScroll.Visible ? HorizontalScroll.Value : 0;
            int currentScrollY = VerticalScroll.Visible ? VerticalScroll.Value : 0;
            int viewportTop = currentScrollY;
            int viewportBottom = currentScrollY + ClientSize.Height;
            int margin = 16;
            int targetScrollY = currentScrollY;

            if (control.Top - margin < viewportTop)
            {
                targetScrollY = Math.Max(0, control.Top - margin);
            }
            else if (control.Bottom + margin > viewportBottom)
            {
                targetScrollY = Math.Max(0, control.Bottom + margin - ClientSize.Height);
            }

            if (targetScrollY == currentScrollY)
            {
                return;
            }

            AutoScrollPosition = new Point(currentScrollX, targetScrollY);

            BeginInvoke(() =>
            {
                if (IsDisposed || !IsHandleCreated || !control.Visible)
                {
                    return;
                }

                AutoScrollPosition = new Point(currentScrollX, targetScrollY);
            });
        });
    }

    private static Rectangle GetImageBounds(Control parent)
    {
        const int imageSize = 200;
        const int topOffset = 5;
        const int absoluteX = 440;

        return new Rectangle(absoluteX, parent.Top + topOffset, imageSize, imageSize);
    }

    private static Label? FindLabelByTag(Control parent, string tag)
    {
        for (int i = 0; i < parent.Controls.Count; i++)
        {
            Control childControl = parent.Controls[i];

            if (childControl is Label label && string.Equals(label.Tag?.ToString(), tag, StringComparison.Ordinal))
            {
                return label;
            }

            Label? nestedLabel = FindLabelByTag(childControl, tag);
            if (nestedLabel != null)
            {
                return nestedLabel;
            }
        }

        return null;
    }

    private static bool EstLabelImage(Label label)
    {
        return string.Equals(label.Text, ImageLabelText, StringComparison.Ordinal)
            || string.Equals(label.Text, ImageIconText, StringComparison.Ordinal);
    }

    private async void WebView21_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
    {
        await Task.CompletedTask;

        if (sender is not Microsoft.Web.WebView2.WinForms.WebView2
            || !e.IsSuccess)
        {
            return;
        }
    }

    #endregion Private
}
